#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DynamicSun.Migrator/DynamicSun.Migrator.csproj", "DynamicSun.Migrator/"]
COPY ["DynamicSun.DataAccess/DynamicSun.DataAccess.csproj", "DynamicSun.DataAccess/"]
COPY ["DynamicSun.Domain/DynamicSun.Domain.csproj", "DynamicSun.Domain/"]
RUN dotnet restore "DynamicSun.Migrator/DynamicSun.Migrator.csproj"
COPY . .
WORKDIR "/src/DynamicSun.Migrator"
RUN dotnet build "DynamicSun.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DynamicSun.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DynamicSun.Migrator.dll"]