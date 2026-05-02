FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["mf-dev-back-end-2026-e2-t1-g5/mf-dev-back-end-2026-e2-t1-g5.csproj", "mf-dev-back-end-2026-e2-t1-g5/"]
RUN dotnet restore "mf-dev-back-end-2026-e2-t1-g5/mf-dev-back-end-2026-e2-t1-g5.csproj"
COPY . .
WORKDIR "/src/mf-dev-back-end-2026-e2-t1-g5"
RUN dotnet build "./mf-dev-back-end-2026-e2-t1-g5.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./mf-dev-back-end-2026-e2-t1-g5.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "mf-dev-back-end-2026-e2-t1-g5.dll"]
