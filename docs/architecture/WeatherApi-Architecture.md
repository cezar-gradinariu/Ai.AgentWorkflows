# Weather API - Architecture Document

**Version:** 1.0  
**Date:** October 20, 2025  
**Project:** Weather API  
**Technology Stack:** .NET 10, C#, MongoDB  

---

## Table of Contents
1. [Executive Summary](#executive-summary)
2. [System Overview](#system-overview)
3. [Architecture Principles](#architecture-principles)
4. [Technology Stack](#technology-stack)
5. [System Architecture](#system-architecture)
6. [Security Architecture](#security-architecture)
7. [Data Architecture](#data-architecture)
8. [API Design](#api-design)
9. [Testing Strategy](#testing-strategy)
10. [Performance & Scalability](#performance--scalability)
11. [Deployment Architecture](#deployment-architecture)
12. [Non-Functional Requirements](#non-functional-requirements)

---

## Executive Summary

The Weather API is a RESTful web service built on .NET 10 that provides weather data to consumers. The API implements a hybrid security model with both public and authenticated endpoints, rate limiting/throttling capabilities, and uses MongoDB as the primary data store. The system follows Clean Architecture principles with comprehensive testing coverage including BDD-style integration tests.

### Key Features
- Public and authenticated API endpoints
- JWT-based authentication and authorization
- Rate limiting and throttling
- MongoDB as the data persistence layer
- Comprehensive unit and integration testing with BDD support
- Containerized deployment support

---

## System Overview

### Purpose
Provide a scalable, secure, and performant API for weather data access with differentiated access levels for public and authenticated users.

### Scope
- RESTful API for weather data operations (CRUD)
- User authentication and authorization
- Rate limiting and throttling per user tier
- Historical weather data storage and retrieval
- Real-time weather data access

### Target Users
1. **Public Users** - Limited access to current weather data
2. **Authenticated Users** - Full access to current and historical data
3. **Premium Users** - Higher rate limits and advanced features
4. **System Administrators** - Full system access and management

---

## Architecture Principles

### 1. Clean Architecture
The system follows Clean Architecture (Onion Architecture) principles:
- **Domain Layer** - Core business logic and entities
- **Application Layer** - Use cases and business rules
- **Infrastructure Layer** - External concerns (database, external APIs)
- **Presentation Layer** - API controllers and endpoints

### 2. SOLID Principles
- Single Responsibility Principle
- Open/Closed Principle
- Liskov Substitution Principle
- Interface Segregation Principle
- Dependency Inversion Principle

### 3. Design Patterns
- **Repository Pattern** - Data access abstraction
- **Unit of Work Pattern** - Transaction management
- **Dependency Injection** - Loose coupling
- **Strategy Pattern** - Rate limiting strategies
- **Factory Pattern** - Object creation
- **Decorator Pattern** - Cross-cutting concerns

### 4. Key Architectural Decisions
- **NoSQL over SQL**: MongoDB chosen for flexible schema and horizontal scalability
- **JWT for Authentication**: Stateless authentication for API scalability
- **API-First Design**: Contract-first approach with OpenAPI/Swagger
- **Testability**: All components designed with testing in mind
- **No Automatic Seeding**: Data seeding only on explicit request

---

## Technology Stack

### Backend Framework
- **.NET 10** - Latest LTS version of .NET
- **ASP.NET Core Web API** - RESTful API framework
- **C# 12** - Latest language features

### Data Layer
- **MongoDB 7.x** - Primary data store
- **MongoDB.Driver** - Official .NET driver
- **MongoDB.Entities** - ODM for simplified data access (optional)

### Authentication & Security
- **JWT Bearer Tokens** - Stateless authentication
- **ASP.NET Core Identity** - User management
- **BCrypt.Net** - Password hashing
- **AspNetCoreRateLimit** - Rate limiting middleware

### Testing
- **xUnit** - Unit testing framework
- **Moq** - Mocking framework
- **FluentAssertions** - Assertion library
- **Reqnroll** (SpecFlow successor) - BDD framework for integration tests
- **Testcontainers** - Container-based integration testing
- **AutoFixture** - Test data generation

### API Documentation
- **Swashbuckle (Swagger)** - API documentation
- **OpenAPI 3.0** - API specification

### Cross-Cutting
- **Serilog** - Structured logging
- **AutoMapper** - Object mapping
- **FluentValidation** - Input validation
- **Polly** - Resilience and transient fault handling

### DevOps & Deployment
- **Docker** - Containerization
- **Docker Compose** - Local development orchestration
- **GitHub Actions** - CI/CD (optional)

---

## System Architecture

### High-Level Architecture Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                        API Clients                          │
│          (Web Apps, Mobile Apps, Third-party)              │
└─────────────────────┬───────────────────────────────────────┘
                      │
                      │ HTTPS
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                   API Gateway / Load Balancer               │
│              (Rate Limiting, SSL Termination)              │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                    Weather API (.NET 10)                    │
│  ┌──────────────────────────────────────────────────────┐  │
│  │         Presentation Layer (API Controllers)         │  │
│  │  - WeatherController  - AuthController               │  │
│  │  - Middleware (Auth, Logging, Exception Handling)    │  │
│  └────────────────────┬─────────────────────────────────┘  │
│  ┌────────────────────▼─────────────────────────────────┐  │
│  │         Application Layer (Services/Use Cases)       │  │
│  │  - WeatherService  - AuthService  - UserService      │  │
│  │  - DTOs  - Validators  - Mappers                     │  │
│  └────────────────────┬─────────────────────────────────┘  │
│  ┌────────────────────▼─────────────────────────────────┐  │
│  │              Domain Layer (Core Logic)               │  │
│  │  - Entities  - Value Objects  - Domain Services      │  │
│  │  - Interfaces  - Business Rules                      │  │
│  └────────────────────┬─────────────────────────────────┘  │
│  ┌────────────────────▼─────────────────────────────────┐  │
│  │        Infrastructure Layer (External Concerns)      │  │
│  │  - Repositories  - MongoDB Context                   │  │
│  │  - External APIs  - File System  - Cache             │  │
│  └──────────────────────────────────────────────────────┘  │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                      MongoDB Cluster                        │
│  - Weather Data Collection                                  │
│  - Users Collection                                         │
│  - API Keys Collection                                      │
│  - Rate Limit Collection                                    │
└─────────────────────────────────────────────────────────────┘
```

### Project Structure

```
WeatherApi/
├── src/
│   ├── WeatherApi.Domain/
│   │   ├── Entities/
│   │   │   ├── WeatherData.cs
│   │   │   ├── User.cs
│   │   │   ├── ApiKey.cs
│   │   │   └── RateLimitCounter.cs
│   │   ├── ValueObjects/
│   │   │   ├── Location.cs
│   │   │   ├── Temperature.cs
│   │   │   └── Coordinates.cs
│   │   ├── Enums/
│   │   │   ├── WeatherCondition.cs
│   │   │   └── UserRole.cs
│   │   ├── Interfaces/
│   │   │   └── IAggregateRoot.cs
│   │   └── Exceptions/
│   │       └── DomainException.cs
│   │
│   ├── WeatherApi.Application/
│   │   ├── Services/
│   │   │   ├── Interfaces/
│   │   │   │   ├── IWeatherService.cs
│   │   │   │   ├── IAuthService.cs
│   │   │   │   └── IUserService.cs
│   │   │   └── Implementations/
│   │   │       ├── WeatherService.cs
│   │   │       ├── AuthService.cs
│   │   │       └── UserService.cs
│   │   ├── DTOs/
│   │   │   ├── Requests/
│   │   │   │   ├── WeatherQueryRequest.cs
│   │   │   │   ├── LoginRequest.cs
│   │   │   │   └── RegisterRequest.cs
│   │   │   └── Responses/
│   │   │       ├── WeatherResponse.cs
│   │   │       ├── AuthResponse.cs
│   │   │       └── ErrorResponse.cs
│   │   ├── Validators/
│   │   │   ├── WeatherQueryValidator.cs
│   │   │   └── LoginRequestValidator.cs
│   │   ├── Mappings/
│   │   │   └── MappingProfile.cs
│   │   └── Interfaces/
│   │       └── ITokenService.cs
│   │
│   ├── WeatherApi.Infrastructure/
│   │   ├── Persistence/
│   │   │   ├── MongoDbContext.cs
│   │   │   ├── MongoDbSettings.cs
│   │   │   └── Configurations/
│   │   │       ├── WeatherDataConfiguration.cs
│   │   │       └── UserConfiguration.cs
│   │   ├── Repositories/
│   │   │   ├── Interfaces/
│   │   │   │   ├── IWeatherRepository.cs
│   │   │   │   ├── IUserRepository.cs
│   │   │   │   └── IRateLimitRepository.cs
│   │   │   └── Implementations/
│   │   │       ├── WeatherRepository.cs
│   │   │       ├── UserRepository.cs
│   │   │       └── RateLimitRepository.cs
│   │   ├── Security/
│   │   │   ├── JwtTokenService.cs
│   │   │   ├── JwtSettings.cs
│   │   │   └── PasswordHasher.cs
│   │   ├── ExternalServices/
│   │   │   └── WeatherApiClient.cs
│   │   └── RateLimiting/
│   │       ├── RateLimitService.cs
│   │       └── RateLimitConfiguration.cs
│   │
│   └── WeatherApi.API/
│       ├── Controllers/
│       │   ├── WeatherController.cs
│       │   ├── AuthController.cs
│       │   └── AdminController.cs
│       ├── Middleware/
│       │   ├── ExceptionHandlingMiddleware.cs
│       │   ├── RateLimitingMiddleware.cs
│       │   └── RequestLoggingMiddleware.cs
│       ├── Filters/
│       │   ├── ValidateModelAttribute.cs
│       │   └── ApiKeyAuthorizationFilter.cs
│       ├── Extensions/
│       │   ├── ServiceCollectionExtensions.cs
│       │   └── ApplicationBuilderExtensions.cs
│       ├── Program.cs
│       └── appsettings.json
│
├── tests/
│   ├── WeatherApi.UnitTests/
│   │   ├── Domain/
│   │   │   └── Entities/
│   │   ├── Application/
│   │   │   ├── Services/
│   │   │   └── Validators/
│   │   ├── Infrastructure/
│   │   │   └── Repositories/
│   │   └── API/
│   │       └── Controllers/
│   │
│   └── WeatherApi.IntegrationTests/
│       ├── Features/
│       │   ├── Weather.feature
│       │   ├── Authentication.feature
│       │   └── RateLimiting.feature
│       ├── StepDefinitions/
│       │   ├── WeatherSteps.cs
│       │   ├── AuthenticationSteps.cs
│       │   └── CommonSteps.cs
│       ├── Support/
│       │   ├── TestWebApplicationFactory.cs
│       │   ├── MongoDbTestContainer.cs
│       │   └── TestDataBuilder.cs
│       └── Hooks/
│           └── TestHooks.cs
│
├── docker/
│   ├── Dockerfile
│   └── docker-compose.yml
│
└── docs/
    ├── api/
    └── architecture/
```

---

## Security Architecture

### Authentication Flow

```
┌────────┐                                    ┌──────────────┐
│ Client │                                    │  Weather API │
└───┬────┘                                    └──────┬───────┘
    │                                                │
    │ POST /api/auth/login                          │
    │ {username, password}                          │
    ├──────────────────────────────────────────────>│
    │                                                │
    │                                         Validate Credentials
    │                                         Generate JWT Token
    │                                                │
    │ 200 OK                                        │
    │ {token, refreshToken, expiresIn}              │
    │<──────────────────────────────────────────────┤
    │                                                │
    │ GET /api/weather?location=London              │
    │ Authorization: Bearer {token}                 │
    ├──────────────────────────────────────────────>│
    │                                                │
    │                                         Validate Token
    │                                         Check Rate Limit
    │                                         Process Request
    │                                                │
    │ 200 OK                                        │
    │ {weather data}                                │
    │<──────────────────────────────────────────────┤
```

### Security Layers

#### 1. Authentication
- **JWT Bearer Tokens** - Stateless authentication
- **Token Expiration** - Short-lived access tokens (15 minutes)
- **Refresh Tokens** - Long-lived tokens for renewal (7 days)
- **Token Revocation** - Blacklist support for compromised tokens

#### 2. Authorization
- **Role-Based Access Control (RBAC)**
  - `Public` - Read-only access to current weather
  - `User` - Full read access to weather data
  - `Premium` - Higher rate limits, bulk operations
  - `Admin` - Full system access

#### 3. API Security
- **HTTPS Only** - TLS 1.3 encryption
- **CORS Policy** - Configurable origins
- **API Key Authentication** - Alternative to JWT for service-to-service
- **Input Validation** - FluentValidation for all inputs
- **SQL/NoSQL Injection Prevention** - Parameterized queries
- **XSS Prevention** - Output encoding

#### 4. Rate Limiting Strategy

```csharp
// Rate Limit Tiers
Public Users:     10 requests/minute,   1000 requests/day
Authenticated:    60 requests/minute,   10000 requests/day
Premium:          300 requests/minute,  100000 requests/day
Admin:            Unlimited
```

---

## Data Architecture

### MongoDB Collections

#### 1. WeatherData Collection
```json
{
  "_id": "ObjectId",
  "location": {
    "city": "string",
    "country": "string",
    "coordinates": {
      "latitude": "decimal",
      "longitude": "decimal"
    }
  },
  "timestamp": "ISODate",
  "temperature": {
    "value": "decimal",
    "unit": "Celsius|Fahrenheit"
  },
  "humidity": "decimal",
  "pressure": "decimal",
  "windSpeed": "decimal",
  "windDirection": "decimal",
  "condition": "string",
  "description": "string",
  "source": "string",
  "createdAt": "ISODate",
  "updatedAt": "ISODate"
}
```

**Indexes:**
- `location.coordinates` - 2dsphere index for geospatial queries
- `timestamp` - Descending for time-series queries
- `location.city, timestamp` - Compound index for city-based queries

#### 2. Users Collection
```json
{
  "_id": "ObjectId",
  "username": "string",
  "email": "string",
  "passwordHash": "string",
  "role": "Public|User|Premium|Admin",
  "isActive": "boolean",
  "apiKeys": ["string"],
  "createdAt": "ISODate",
  "updatedAt": "ISODate",
  "lastLoginAt": "ISODate"
}
```

**Indexes:**
- `username` - Unique index
- `email` - Unique index
- `apiKeys` - For API key lookup

#### 3. RateLimitCounters Collection
```json
{
  "_id": "ObjectId",
  "userId": "ObjectId",
  "endpoint": "string",
  "requestCount": "int",
  "windowStart": "ISODate",
  "windowEnd": "ISODate",
  "createdAt": "ISODate"
}
```

**Indexes:**
- `userId, windowStart` - Compound index
- `windowEnd` - TTL index for automatic cleanup

#### 4. RefreshTokens Collection
```json
{
  "_id": "ObjectId",
  "userId": "ObjectId",
  "token": "string",
  "expiresAt": "ISODate",
  "isRevoked": "boolean",
  "createdAt": "ISODate"
}
```

**Indexes:**
- `token` - Unique index
- `expiresAt` - TTL index

### Data Seeding Strategy

**NO AUTOMATIC SEEDING** - Data seeding only occurs when explicitly requested:

1. **Development Environment**
   - Seed via explicit command: `dotnet run --seed`
   - Or via dedicated endpoint: `POST /api/admin/seed` (Admin only)

2. **Production Environment**
   - No automatic seeding
   - Data import via admin tools only

3. **Test Environment**
   - Seed data created per test via test fixtures
   - Cleaned up after test execution

---

## API Design

### RESTful Endpoints

#### Public Endpoints (No Authentication Required)

```
GET  /api/weather/current?city={city}&country={country}
GET  /api/weather/current/coordinates?lat={lat}&lon={lon}
GET  /api/health
GET  /api/version
```

#### Authenticated Endpoints (JWT Required)

```
# Weather Operations
GET    /api/weather/history?city={city}&from={date}&to={date}
GET    /api/weather/forecast?city={city}&days={days}
POST   /api/weather/bulk
GET    /api/weather/search?query={query}

# User Operations
GET    /api/users/profile
PUT    /api/users/profile
GET    /api/users/usage-statistics
POST   /api/users/api-keys
DELETE /api/users/api-keys/{keyId}
```

#### Authentication Endpoints

```
POST   /api/auth/register
POST   /api/auth/login
POST   /api/auth/refresh
POST   /api/auth/logout
POST   /api/auth/forgot-password
POST   /api/auth/reset-password
```

#### Admin Endpoints (Admin Role Required)

```
GET    /api/admin/users
POST   /api/admin/users/{id}/role
DELETE /api/admin/users/{id}
GET    /api/admin/statistics
POST   /api/admin/seed (explicit data seeding)
GET    /api/admin/rate-limits
POST   /api/admin/rate-limits/reset
```

### API Versioning

- **URL Versioning**: `/api/v1/weather/current`
- **Header Versioning**: `X-API-Version: 1.0` (alternative)

### Standard Response Format

#### Success Response
```json
{
  "success": true,
  "data": { },
  "timestamp": "2025-10-20T10:30:00Z"
}
```

#### Error Response
```json
{
  "success": false,
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Invalid input parameters",
    "details": [
      {
        "field": "city",
        "message": "City name is required"
      }
    ]
  },
  "timestamp": "2025-10-20T10:30:00Z"
}
```

#### Rate Limit Headers
```
X-RateLimit-Limit: 60
X-RateLimit-Remaining: 45
X-RateLimit-Reset: 1698154800
```

---

## Testing Strategy

### 1. Unit Tests (WeatherApi.UnitTests)

**Framework**: xUnit, Moq, FluentAssertions, AutoFixture

**Coverage Areas**:
- Domain entities and value objects
- Application services
- Validators
- Repository implementations
- Controllers

**Principles**:
- Test isolation (no dependencies)
- Fast execution (< 5 seconds for entire suite)
- Mock external dependencies
- AAA pattern (Arrange, Act, Assert)
- One assertion per test (where practical)

**Example Structure**:
```csharp
public class WeatherServiceTests
{
    private readonly Mock<IWeatherRepository> _repositoryMock;
    private readonly Mock<ILogger<WeatherService>> _loggerMock;
    private readonly WeatherService _sut;
    
    public WeatherServiceTests()
    {
        _repositoryMock = new Mock<IWeatherRepository>();
        _loggerMock = new Mock<ILogger<WeatherService>>();
        _sut = new WeatherService(_repositoryMock.Object, _loggerMock.Object);
    }
    
    [Fact]
    public async Task GetCurrentWeather_WithValidCity_ReturnsWeatherData()
    {
        // Arrange
        var city = "London";
        var expectedWeather = new WeatherData { /* ... */ };
        _repositoryMock.Setup(r => r.GetLatestByCityAsync(city))
                      .ReturnsAsync(expectedWeather);
        
        // Act
        var result = await _sut.GetCurrentWeatherAsync(city);
        
        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedWeather);
    }
}
```

### 2. Integration Tests (WeatherApi.IntegrationTests)

**Framework**: xUnit, Reqnroll (BDD), Testcontainers, FluentAssertions

**Coverage Areas**:
- End-to-end API workflows
- Database integration
- Authentication and authorization
- Rate limiting
- Error handling

**Technologies**:
- **Testcontainers**: Spin up real MongoDB container
- **Reqnroll**: BDD scenarios in Gherkin
- **WebApplicationFactory**: In-memory API hosting

**BDD Feature Example** (`Weather.feature`):
```gherkin
Feature: Weather Data Retrieval
  As an API consumer
  I want to retrieve weather data
  So that I can display it in my application

  Background:
    Given the Weather API is running
    And the database is clean

  Scenario: Public user retrieves current weather
    Given I am a public user
    When I request current weather for "London"
    Then I should receive a 200 OK response
    And the response should contain weather data for "London"

  Scenario: Authenticated user retrieves historical weather
    Given I am an authenticated user
    And historical weather data exists for "Paris" from "2025-10-01" to "2025-10-10"
    When I request historical weather for "Paris" from "2025-10-01" to "2025-10-10"
    Then I should receive a 200 OK response
    And the response should contain 10 weather data points

  Scenario: Rate limit is enforced for public users
    Given I am a public user
    And I have made 10 requests in the last minute
    When I make another request for current weather
    Then I should receive a 429 Too Many Requests response
    And the response should include rate limit headers

  Scenario: Unauthenticated user cannot access historical data
    Given I am a public user
    When I request historical weather for "Berlin"
    Then I should receive a 401 Unauthorized response
```

**Test Container Setup**:
```csharp
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MongoDbContainer _mongoContainer;
    
    public TestWebApplicationFactory()
    {
        _mongoContainer = new MongoDbBuilder()
            .WithImage("mongo:7.0")
            .WithCleanUp(true)
            .Build();
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _mongoContainer.StartAsync().Wait();
        
        builder.ConfigureServices(services =>
        {
            // Replace MongoDB connection with test container
            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = _mongoContainer.GetConnectionString();
            });
        });
    }
}
```

### Test Coverage Goals
- **Unit Tests**: > 85% code coverage
- **Integration Tests**: All critical user journeys
- **Performance Tests**: Response time < 200ms (p95)

---

## Performance & Scalability

### Performance Requirements

| Metric | Target | Maximum |
|--------|--------|---------|
| Response Time (p50) | < 50ms | < 100ms |
| Response Time (p95) | < 200ms | < 500ms |
| Response Time (p99) | < 500ms | < 1000ms |
| Throughput | > 1000 req/s | - |
| Availability | 99.9% | - |

### Scalability Strategies

#### 1. Horizontal Scaling
- Stateless API design
- Load balancing across multiple instances
- MongoDB replica sets for read scaling

#### 2. Caching Strategy
- **Redis/Memory Cache** for frequently accessed data
- Cache current weather data (TTL: 5 minutes)
- Cache user profile data (TTL: 15 minutes)
- Cache rate limit counters (in-memory)

#### 3. Database Optimization
- Proper indexing strategy
- Query optimization
- Connection pooling
- Read preference for read-heavy operations

#### 4. Rate Limiting
- Distributed rate limiting with Redis
- Sliding window algorithm
- Per-user and per-IP tracking

#### 5. Asynchronous Processing
- Background jobs for data aggregation
- Message queue for non-critical operations (optional)

---

## Deployment Architecture

### Container Strategy

**Dockerfile** (.NET 10):
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
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
```

**Docker Compose** (Local Development):
```yaml
version: '3.8'

services:
  weatherapi:
    build:
      context: .
      dockerfile: docker/Dockerfile
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - MongoDbSettings__ConnectionString=mongodb://mongodb:27017
      - MongoDbSettings__DatabaseName=WeatherApiDb
      - JwtSettings__SecretKey=${JWT_SECRET}
    depends_on:
      - mongodb
    networks:
      - weather-network

  mongodb:
    image: mongo:7.0
    ports:
      - "27017:27017"
    environment:
      - MONGO_INITDB_ROOT_USERNAME=admin
      - MONGO_INITDB_ROOT_PASSWORD=password123
    volumes:
      - mongodb_data:/data/db
    networks:
      - weather-network

  mongo-express:
    image: mongo-express:latest
    ports:
      - "8081:8081"
    environment:
      - ME_CONFIG_MONGODB_SERVER=mongodb
      - ME_CONFIG_MONGODB_ADMINUSERNAME=admin
      - ME_CONFIG_MONGODB_ADMINPASSWORD=password123
    depends_on:
      - mongodb
    networks:
      - weather-network

volumes:
  mongodb_data:

networks:
  weather-network:
    driver: bridge
```

### Environment Configuration

#### Development
- Local MongoDB instance
- Detailed logging
- Swagger UI enabled
- CORS open for localhost

#### Staging
- MongoDB replica set
- Info-level logging
- Swagger UI enabled with authentication
- Restricted CORS

#### Production
- MongoDB sharded cluster
- Warning-level logging
- Swagger UI disabled (or secured)
- Strict CORS policy
- HTTPS only

---

## Non-Functional Requirements

### 1. Security
- All sensitive data encrypted at rest and in transit
- Regular security audits
- Dependency vulnerability scanning
- OWASP Top 10 compliance

### 2. Reliability
- 99.9% uptime SLA
- Graceful degradation
- Circuit breaker pattern for external dependencies
- Automatic retry with exponential backoff

### 3. Maintainability
- Clean code principles
- Comprehensive documentation
- Code reviews mandatory
- Automated code quality checks

### 4. Observability
- Structured logging (Serilog)
- Application metrics (response times, error rates)
- Health checks
- Distributed tracing (optional: OpenTelemetry)

### 5. Monitoring & Alerting
- API response time monitoring
- Error rate tracking
- Rate limit threshold alerts
- Database performance monitoring

### 6. Compliance
- GDPR compliance for user data
- Data retention policies
- Right to be forgotten implementation
- Audit logging for sensitive operations

---

## Implementation Phases

### Phase 1: Foundation (Weeks 1-2)
- [x] Project structure setup
- [ ] Domain model implementation
- [ ] MongoDB integration
- [ ] Basic CRUD operations
- [ ] Unit test framework setup

### Phase 2: Core Features (Weeks 3-4)
- [ ] Authentication & authorization
- [ ] JWT implementation
- [ ] API endpoints implementation
- [ ] Input validation
- [ ] Error handling

### Phase 3: Advanced Features (Weeks 5-6)
- [ ] Rate limiting implementation
- [ ] Caching layer
- [ ] Integration tests with Testcontainers
- [ ] BDD scenarios with Reqnroll
- [ ] API documentation

### Phase 4: Production Readiness (Weeks 7-8)
- [ ] Performance optimization
- [ ] Security hardening
- [ ] Monitoring and logging
- [ ] Docker containerization
- [ ] CI/CD pipeline

### Phase 5: Launch & Iteration (Week 9+)
- [ ] Production deployment
- [ ] Monitoring and alerting setup
- [ ] User feedback collection
- [ ] Iterative improvements

---

## Appendices

### A. Technology Decisions

| Decision | Rationale |
|----------|-----------|
| MongoDB | Flexible schema, horizontal scalability, geospatial queries |
| .NET 10 | Latest features, performance improvements, long-term support |
| JWT | Stateless authentication, scalability, industry standard |
| Reqnroll | Modern BDD framework, SpecFlow successor, .NET 8+ support |
| Testcontainers | Real environment testing, consistency across environments |
| Clean Architecture | Maintainability, testability, separation of concerns |

### B. Code Quality Standards
- **Naming Conventions**: PascalCase for public members, camelCase for private
- **File Organization**: One class per file
- **Using Statements**: Always at the top of the file
- **Maximum Method Length**: 30 lines
- **Cyclomatic Complexity**: < 10
- **Test Coverage**: > 85%

### C. Coding Rules
1. All `using` statements must be at the top of the file
2. No data seeding unless explicitly requested
3. All public APIs must have XML documentation
4. All inputs must be validated
5. All exceptions must be logged
6. All database operations must be async
7. All tests must follow AAA pattern

### D. API Response Codes

| Code | Meaning | Usage |
|------|---------|-------|
| 200 | OK | Successful request |
| 201 | Created | Resource created successfully |
| 204 | No Content | Successful delete operation |
| 400 | Bad Request | Invalid input |
| 401 | Unauthorized | Missing or invalid authentication |
| 403 | Forbidden | Insufficient permissions |
| 404 | Not Found | Resource not found |
| 429 | Too Many Requests | Rate limit exceeded |
| 500 | Internal Server Error | Unexpected server error |
| 503 | Service Unavailable | Temporary unavailability |

---

## Document Revision History

| Version | Date | Author | Changes |
|---------|------|--------|---------|
| 1.0 | 2025-10-20 | Architect Agent | Initial architecture document |

---

**Document Status**: ✅ Approved  
**Next Review Date**: 2025-11-20  
**Owner**: Architecture Team

