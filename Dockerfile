# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src

COPY src/FiapCloudGames.Notifications.Application/FiapCloudGames.Notifications.Application.csproj ./FiapCloudGames.Notifications.Application/
COPY src/FiapCloudGames.Notifications.Infrastructure/FiapCloudGames.Notifications.Infrastructure.csproj ./FiapCloudGames.Notifications.Infrastructure/
COPY src/FiapCloudGames.Notifications.API/FiapCloudGames.Notifications.API.csproj ./FiapCloudGames.Notifications.API/

RUN dotnet restore ./FiapCloudGames.Notifications.API/FiapCloudGames.Notifications.API.csproj

COPY src/FiapCloudGames.Notifications.Application/ ./FiapCloudGames.Notifications.Application/
COPY src/FiapCloudGames.Notifications.Infrastructure/ ./FiapCloudGames.Notifications.Infrastructure/
COPY src/FiapCloudGames.Notifications.API/ ./FiapCloudGames.Notifications.API/

WORKDIR /src/FiapCloudGames.Notifications.API
RUN dotnet build FiapCloudGames.Notifications.API.csproj -c Release --no-restore

# Stage 2: Publicação
FROM build AS publish
RUN dotnet publish FiapCloudGames.Notifications.API.csproj -c Release --no-build -o /app/publish

# Stage 3: Runtime (Final)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

RUN addgroup -g 1000 -S appgroup && \
    adduser -u 1000 -S appuser -G appgroup

RUN apk add --no-cache \
    icu-libs \
    tzdata \
    ca-certificates

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV TZ=America/Sao_Paulo

RUN mkdir -p /app/logs && chown -R appuser:appgroup /app/logs

COPY --from=publish --chown=appuser:appgroup /app/publish .

USER appuser

EXPOSE 8080

ENTRYPOINT ["dotnet", "FiapCloudGames.Notifications.API.dll"]
