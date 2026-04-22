FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Projeleri kopyala
COPY ["src/App.API/App.API.csproj", "App.API/"]
COPY ["src/App.BL/App.BL.csproj", "App.BL/"]
COPY ["src/App.DAL/App.DAL.csproj", "App.DAL/"]
COPY ["src/App.Core/App.Core.csproj", "App.Core/"]

# Restore yap
RUN dotnet restore "App.API/App.API.csproj"

# Tüm kodları kopyala
COPY . .

# Build - Klasörü değiştirip oradan build alalım
WORKDIR "/src/App.API"
RUN dotnet build "App.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "App.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false