FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MainApp/MainApp.csproj", "MainApp/"]
RUN dotnet restore "MainApp/MainApp.csproj"
COPY . .
WORKDIR "/src/MainApp"
RUN dotnet build "MainApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MainApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MainApp.dll"]
