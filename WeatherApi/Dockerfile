FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/WeatherApi.API/WeatherApi.API.csproj", "src/WeatherApi.API/"]
COPY ["src/WeatherApi.Application/WeatherApi.Application.csproj", "src/WeatherApi.Application/"]
COPY ["src/WeatherApi.Domain/WeatherApi.Domain.csproj", "src/WeatherApi.Domain/"]
COPY ["src/WeatherApi.Infrastructure/WeatherApi.Infrastructure.csproj", "src/WeatherApi.Infrastructure/"]
RUN dotnet restore "src/WeatherApi.API/WeatherApi.API.csproj"
COPY . .
WORKDIR "/src/src/WeatherApi.API"
RUN dotnet build "WeatherApi.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherApi.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeatherApi.API.dll"]

