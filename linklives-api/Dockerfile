#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["linklives-api/linklives-api.csproj", "linklives-api/"]
COPY ["linklives-api-dal/linklives-api-dal.csproj", "linklives-api-dal/"]
RUN dotnet restore "linklives-api/linklives-api.csproj"
COPY . .
WORKDIR "/src/linklives-api"
RUN dotnet build "linklives-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "linklives-api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "linklives-api.dll"]