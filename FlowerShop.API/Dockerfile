FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FlowerShop.API/FlowerShop.API.csproj", "FlowerShop.API/"]
COPY ["FlowerShop.DependencyInjection/FlowerShop.DependencyInjection.csproj", "FlowerShop.DependencyInjection/"]
COPY ["FlowerShop.ApplicationService/FlowerShop.ApplicationService.csproj", "FlowerShop.ApplicationService/"]
COPY ["FlowerShop.Infrastructure/FlowerShop.Infrastructure.csproj", "FlowerShop.Infrastructure/"]
COPY ["FlowerShop.Business/FlowerShop.Business.csproj", "FlowerShop.Business/"]
COPY ["FlowerShop.Domain/FlowerShop.Domain.csproj", "FlowerShop.Domain/"]
RUN dotnet restore "./FlowerShop.API/FlowerShop.API.csproj"
COPY . .
WORKDIR "/src/FlowerShop.API"
RUN dotnet build "./FlowerShop.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FlowerShop.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlowerShop.API.dll"]