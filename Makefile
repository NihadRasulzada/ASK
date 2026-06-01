# ============================================================
#  ASK — Makefile
#  Usage: make <target>
# ============================================================

# ---------- Variables ---------------------------------------
SOLUTION        := App.slnx
API_PROJECT     := src/App.API/App.API.csproj
CONFIGURATION   ?= Debug
VERBOSITY       ?= minimal

# Startup project used by EF tooling
EF_STARTUP := $(API_PROJECT)

# Docker
COMPOSE_FILE ?= docker-compose.yml

# Colours
CYAN  := \033[0;36m
RESET := \033[0m
BOLD  := \033[1m

.PHONY: help \
        build build-release clean restore \
        run watch \
        test test-coverage \
        migrate-add migrate-up migrate-down \
        docker-up docker-down docker-build docker-logs \
        lint format \
        check-tools

# ============================================================
#  DEFAULT — help
# ============================================================
help: ## Show this help message
	@echo ""
	@echo "$(BOLD)NexActor — available targets$(RESET)"
	@echo "----------------------------------------------"
	@awk 'BEGIN {FS = ":.*##"} /^[a-zA-Z_-]+:.*##/ \
	  { printf "  $(CYAN)%-22s$(RESET) %s\n", $$1, $$2 }' $(MAKEFILE_LIST)
	@echo ""

# ============================================================
#  BUILD
# ============================================================
restore: ## Restore NuGet packages
	dotnet restore $(SOLUTION)

build: restore ## Build solution (Debug)
	dotnet build $(SOLUTION) \
	  --configuration $(CONFIGURATION) \
	  --no-restore \
	  --verbosity $(VERBOSITY)

build-release: ## Build solution (Release)
	$(MAKE) build CONFIGURATION=Release

clean: ## Remove bin / obj artefacts
	dotnet clean $(SOLUTION) --verbosity $(VERBOSITY)
	find . -type d \( -name bin -o -name obj \) \
	       -not -path "*/node_modules/*" \
	       -exec rm -rf {} + 2>/dev/null || true
	@echo "$(CYAN)Clean complete.$(RESET)"

# ============================================================
#  RUN / WATCH
# ============================================================
run: build ## Run the API (Debug)
	dotnet run --project $(API_PROJECT) \
	  --configuration $(CONFIGURATION) \
	  --no-build

watch: ## Run the API with hot-reload
	dotnet watch --project $(API_PROJECT) run

# ============================================================
#  EF CORE MIGRATIONS
#
#  Usage examples:
#    make migrate-add MODULE=Builder NAME=InitialCreate
#    make migrate-up  MODULE=Identity
#    make migrate-down MODULE=Project
# ============================================================
MODULE ?= Builder

_persistence_path = $(shell \
  case "$(MODULE)" in \
    Builder)  echo "$(BUILDER_PERSISTENCE)"  ;; \
    Identity) echo "$(IDENTITY_PERSISTENCE)" ;; \
    Project)  echo "$(PROJECT_PERSISTENCE)"  ;; \
    *) echo ""; \
  esac)

migrate-add: ## Add migration  — MODULE=<Builder|Identity|Project> NAME=<MigrationName>
ifndef NAME
	$(error NAME is required. Example: make migrate-add MODULE=Builder NAME=InitialCreate)
endif
	dotnet ef migrations add $(NAME) \
	  --project $(_persistence_path) \
	  --startup-project $(EF_STARTUP) \
	  --verbose

migrate-up: ## Apply pending migrations — MODULE=<Builder|Identity|Project>
	dotnet ef database update \
	  --project $(_persistence_path) \
	  --startup-project $(EF_STARTUP) \
	  --verbose

migrate-down: ## Revert last migration — MODULE=<Builder|Identity|Project>
	dotnet ef migrations remove \
	  --project $(_persistence_path) \
	  --startup-project $(EF_STARTUP) \
	  --force


# ============================================================
#  CODE QUALITY
# ============================================================
format: ## Format code with dotnet-format
	dotnet format $(SOLUTION) --verbosity $(VERBOSITY)

lint: ## Check formatting without applying changes
	dotnet format $(SOLUTION) --verify-no-changes --verbosity $(VERBOSITY)

# ============================================================
#  CHECKS
# ============================================================
check-tools: ## Verify required CLI tools are installed
	@echo "$(CYAN)Checking required tools...$(RESET)"
	@command -v dotnet  >/dev/null 2>&1 && echo "  ✔ dotnet"  || echo "  ✘ dotnet  (not found)"
	@command -v docker  >/dev/null 2>&1 && echo "  ✔ docker"  || echo "  ✘ docker  (not found)"
	@dotnet tool list -g | grep -q "dotnet-ef"     && echo "  ✔ dotnet-ef"     || echo "  ✘ dotnet-ef     → dotnet tool install -g dotnet-ef"
	@dotnet tool list -g | grep -q "dotnet-format" && echo "  ✔ dotnet-format" || echo "  ✘ dotnet-format → dotnet tool install -g dotnet-format"
