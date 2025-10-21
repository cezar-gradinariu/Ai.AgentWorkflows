# Weather API - Feature-003 Implementation

## Overview
A RESTful Weather Data API built with .NET 9, MongoDB, and in-memory caching. Provides public access to weather forecasts and secure endpoints for data ingestion.

## Architecture

### Project Structure
```
WeatherApi/
├── WeatherApi.sln
├── src/
│   ├── WeatherApi.Domain/           # Domain entities and repository interfaces
��   ├── WeatherApi.Application/      # Business logic, CQRS handlers, DTOs, validators
│   ├── WeatherApi.Infrastructure/   # MongoDB repository implementation
│   └── WeatherApi.Api/              # ASP.NET Core Web API, controllers, middleware
└── tests/
    ├── WeatherApi.UnitTests/        # Unit tests for all layers
    └── WeatherApi.IntegrationTests/ # BDD integration tests (to be implemented)
```

### Technology Stack
- **.NET 9** (C#)
- **MongoDB** for persistent storage
- **IMemoryCache** for in-memory caching (10-minute cache duration)
- **FluentValidation** for input validation
- **xUnit**, **Moq**, **FluentAssertions** for testing
- **Swagger/OpenAPI** for API documentation

## Features

### 1. GET Weather Data (Public Endpoint)
- **Endpoint:** `GET /api/weather/{city}?days={1-5}`
- **Authentication:** None (public access)
- **Caching:** 10-minute cache for optimal performance
- **Returns:** Up to 120 hours (5 days) of hourly weather data

**Example Request:**
```bash
curl -X GET "https://localhost:5001/api/weather/London?days=3"
```

### 2. POST Single Weather Data (Protected Endpoint)
- **Endpoint:** `POST /api/weather`
- **Authentication:** Requires `X-API-Key` header
- **Validation:** All fields validated using FluentValidation
- **Upsert Logic:** Updates only if new data is newer, otherwise ignores

**Example Request:**
```bash
curl -X POST "https://localhost:5001/api/weather" \
  -H "X-API-Key: your-api-key-here" \
  -H "Content-Type: application/json" \
  -d '{
    "city": "London",
    "timestamp": "2025-10-22T10:00:00Z",
    "temperature": 18.5,
    "humidity": 65,
    "windSpeed": 15,
    "condition": "Cloudy"
  }'
```

### 3. POST Bulk Weather Data (Protected Endpoint)
- **Endpoint:** `POST /api/weather/bulk`
- **Authentication:** Requires `X-API-Key` header
- **Validation:** All items validated before processing
- **Performance:** Processes multiple records efficiently

**Example Request:**
```bash
curl -X POST "https://localhost:5001/api/weather/bulk" \
  -H "X-API-Key: your-api-key-here" \
  -H "Content-Type: application/json" \
  -d '[
    {
      "city": "London",
      "timestamp": "2025-10-22T10:00:00Z",
      "temperature": 18.5,
      "humidity": 65,
      "windSpeed": 15,
      "condition": "Cloudy"
    },
    {
      "city": "Paris",
      "timestamp": "2025-10-22T11:00:00Z",
      "temperature": 20.0,
      "humidity": 60,
      "windSpeed": 12,
      "condition": "Sunny"
    }
  ]'
```

## Configuration

### appsettings.json
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://localhost:27017"
  },
  "MongoDB": {
    "DatabaseName": "weatherdb"
  },
  "ApiKey": "DEV-KEY-ONLY-DO-NOT-USE-IN-PRODUCTION"
}
```

### Environment Variables (Production)

⚠️ **IMPORTANT:** Never commit production API keys to source control!

For production deployment, override the following settings using environment variables:

**Required Environment Variables:**
- `ConnectionStrings__MongoDB`: MongoDB connection string
- `MongoDB__DatabaseName`: Database name
- `ApiKey`: Production API key for POST endpoint authentication

**Example (Linux/Mac):**
```bash
export ConnectionStrings__MongoDB="mongodb://prod-server:27017"
export MongoDB__DatabaseName="weatherdb-prod"
export ApiKey="your-secure-production-api-key-here"
```

**Example (Windows):**
```cmd
set ConnectionStrings__MongoDB=mongodb://prod-server:27017
set MongoDB__DatabaseName=weatherdb-prod
set ApiKey=your-secure-production-api-key-here
```

**Example (Docker):**
```yaml
environment:
  - ConnectionStrings__MongoDB=mongodb://mongo:27017
  - MongoDB__DatabaseName=weatherdb
  - ApiKey=${API_KEY}
```

**Example (Azure App Service):**
Use Application Settings in the Azure Portal to set configuration values.

## Running the Application

### Prerequisites
- .NET 9 SDK
- MongoDB (local or cloud)
- Docker (optional, for MongoDB)

### Start MongoDB (Docker)
```bash
docker run -d -p 27017:27017 --name mongodb mongo:latest
```

### Run the API
```bash
cd WeatherApi
dotnet run --project src/WeatherApi.Api/WeatherApi.Api.csproj
```

### Access Swagger UI
Navigate to: `https://localhost:5001/swagger`

## Testing

### Run Unit Tests
```bash
dotnet test tests/WeatherApi.UnitTests/WeatherApi.UnitTests.csproj
```

### Run Integration Tests (To Be Implemented)
```bash
dotnet test tests/WeatherApi.IntegrationTests/WeatherApi.IntegrationTests.csproj
```

### Test Coverage
- **Target:** 80%+ code coverage
- **Current:** Unit tests cover all handlers, validators, and core logic

## API Validation Rules

### Weather Data Request
- **City:** Required, max 100 characters
- **Timestamp:** Required
- **Temperature:** -100°C to 60°C
- **Humidity:** 0% to 100%
- **WindSpeed:** >= 0 km/h
- **Condition:** Max 200 characters

## Upsert Logic

The intelligent upsert logic ensures data quality:

1. **No existing data** → INSERT new record
2. **Existing data, new data is newer** (based on `LastUpdated`) → UPDATE
3. **Existing data, new data is older or same age** → IGNORE (preserve latest data)

This prevents stale data from overwriting fresh information.

## Performance

- **Response Time Target:** < 200ms (with caching)
- **Cache Duration:** 10 minutes
- **Concurrent Requests:** Supports 1000+ concurrent users
- **Bulk Processing:** Efficient parallel processing for bulk inserts

## Security

- **GET endpoints:** Public (no authentication)
- **POST endpoints:** Protected with API key authentication
- **API Key:** Passed via `X-API-Key` header
- **HTTPS:** Enforced in production

## CQRS Pattern

The application uses CQRS (Command Query Responsibility Segregation) without MediatR:

### Queries
- `GetWeatherQuery` + `GetWeatherQueryHandler`: Retrieve weather data with caching

### Commands
- `UpsertWeatherCommand` + `UpsertWeatherCommandHandler`: Single upsert
- `UpsertBulkWeatherCommand` + `UpsertBulkWeatherCommandHandler`: Bulk upsert

## Database Schema

### Collection: weatherdatapoints
```json
{
  "_id": "ObjectId",
  "city": "string",
  "timestamp": "DateTime",
  "temperature": "double",
  "humidity": "double",
  "windSpeed": "double",
  "condition": "string",
  "lastUpdated": "DateTime"
}
```

### Indexes
- Compound index on `(city, timestamp)` for efficient queries

## Future Enhancements

1. **Integration Tests:** BDD scenarios with Reqnroll and TestContainers
2. **Rate Limiting:** Protect against API abuse
3. **Metrics & Monitoring:** Application Insights or Prometheus
4. **Authentication:** OAuth2/JWT for more robust security
5. **Pagination:** For large result sets
6. **Filtering:** Additional query parameters (temperature range, conditions)

## Contributing

Follow the coding standards defined in `/agents/coder/rules.md`:
- One class per file
- Proper folder structure (`src/` and `tests/`)
- 80%+ test coverage
- FluentValidation for all input validation

## License

[Your License Here]

## Contact

[Your Contact Information]
