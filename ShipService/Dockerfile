FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShipService/ShipService.csproj", "ShipService/"]
RUN dotnet restore "ShipService/ShipService.csproj"
COPY . .
WORKDIR "/src/ShipService"
RUN dotnet build "ShipService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShipService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipService.dll"]
