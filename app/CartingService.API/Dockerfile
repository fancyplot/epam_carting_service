#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CartingService.API/CartingService.API.csproj", "CartingService.API/"]
COPY ["CartingService.Domain/CartingService.Domain.csproj", "CartingService.Domain/"]
COPY ["CartingService.Infrastructure/CartingService.Infrastructure.csproj", "CartingService.Infrastructure/"]
COPY ["OnlineStore.Contracts/OnlineStore.Contracts.csproj", "OnlineStore.Contracts/"]
RUN dotnet restore "CartingService.API/CartingService.API.csproj"
COPY . .
WORKDIR "/src/CartingService.API"
RUN dotnet build "CartingService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CartingService.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartingService.API.dll"]