# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Sadece proje dosyalarını doğru klasörlere kopyala
COPY ["src/App.API/App.API.csproj", "App.API/"]
COPY ["src/App.BL/App.BL.csproj", "App.BL/"]
COPY ["src/App.DAL/App.DAL.csproj", "App.DAL/"]
COPY ["src/App.Core/App.Core.csproj", "App.Core/"]

# Restore işlemini yap
RUN dotnet restore "App.API/App.API.csproj"

# HATA BURADAYDI: Bilgisayardaki src klasörünün içeriğini, container'daki /src klasörüne kopyala
COPY src/ .

# Build işlemi için API klasörüne gir
WORKDIR "/src/App.API"
RUN dotnet build "App.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "App.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App.API.dll"]