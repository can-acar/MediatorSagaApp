FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MessageQueueService/MessageQueueService.csproj", "MessageQueueService/"]
RUN dotnet restore "MessageQueueService/MessageQueueService.csproj"
COPY . .
WORKDIR "/src/MessageQueueService"
RUN dotnet build "MessageQueueService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageQueueService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageQueueService.dll"]
