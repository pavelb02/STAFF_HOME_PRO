# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем весь src
COPY src .

# Restore всех зависимостей через csproj API
RUN dotnet restore "Services/Personnel/Api/Personnel.Api/Personnel.Api.csproj"

# Publish API
WORKDIR /src/Services/Personnel/Api/Personnel.Api
RUN dotnet publish "Personnel.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "Personnel.Api.dll"]
