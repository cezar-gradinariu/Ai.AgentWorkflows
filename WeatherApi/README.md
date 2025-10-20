# Weather API

A RESTful API built with .NET 8, MongoDB, JWT authentication, and comprehensive testing support.

## Architecture

This project follows **Clean Architecture** principles with the following layers:

- **Domain Layer** - Core business entities and interfaces
- **Application Layer** - Business logic, services, DTOs, and validators
- **Infrastructure Layer** - MongoDB repositories, JWT token service, password hashing
- **API Layer** - Controllers, middleware, and API configuration

## Features

- ✅ JWT-based authentication and authorization
- ✅ Role-based access control (Public, User, Premium, Admin)
- ✅ MongoDB as the data store
- ✅ Password hashing with BCrypt
- ✅ FluentValidation for request validation
- ✅ Serilog for structured logging
- ✅ Swagger/OpenAPI documentation
- ✅ Custom exception handling middleware
- ✅ Request logging middleware
- ✅ Docker support with Docker Compose
- ✅ Health check endpoints
- ✅ Unit and Integration testing support

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MongoDB](https://www.mongodb.com/try/download/community) (or use Docker Compose)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for containerized deployment)

## Getting Started

### Option 1: Run with Docker Compose (Recommended)

1. Clone the repository
2. Navigate to the project directory
3. Run the application with Docker Compose:

```bash
docker-compose up --build
```

The API will be available at:
- API: http://localhost:5000
- Swagger UI: http://localhost:5000/swagger
- Mongo Express: http://localhost:8081 (username: admin, password: admin)

### Option 2: Run Locally

1. Install MongoDB locally or ensure you have access to a MongoDB instance

2. Update the connection string in `appsettings.Development.json`:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "WeatherApiDb_Dev"
  }
}
```

3. Build and run the application:

```bash
cd WeatherApi
dotnet build
dotnet run --project src/WeatherApi.API/WeatherApi.API.csproj
```

The API will be available at:
- HTTPS: https://localhost:5001
- HTTP: http://localhost:5000
- Swagger UI: https://localhost:5001/swagger

## API Endpoints

### Health Checks (Public)

- `GET /api/health` - Basic health check
- `GET /api/health/ready` - Readiness check
- `GET /api/health/live` - Liveness check

### Authentication (Public)

- `POST /api/auth/register` - Register a new user
- `POST /api/auth/login` - Login and get JWT token
- `POST /api/auth/refresh` - Refresh access token
- `POST /api/auth/logout` - Logout (revoke refresh tokens) [Authenticated]
- `POST /api/auth/revoke` - Revoke a specific refresh token [Authenticated]

### User Management (Authenticated)

- `GET /api/user/profile` - Get current user profile
- `PUT /api/user/profile/email` - Update user email
- `DELETE /api/user/deactivate` - Deactivate user account

## Usage Examples

### 1. Register a New User

```bash
curl -X POST "http://localhost:5000/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "email": "testuser@example.com",
    "password": "Test@1234",
    "confirmPassword": "Test@1234"
  }'
```

**Response:**
```json
{
  "success": true,
  "data": {
    "token": "eyJhbGciOiJIUzI1NiIs...",
    "refreshToken": "base64encodedtoken...",
    "expiresAt": "2025-10-20T11:45:00Z",
    "username": "testuser",
    "email": "testuser@example.com",
    "role": "User"
  },
  "timestamp": "2025-10-20T11:30:00Z"
}
```

### 2. Login

```bash
curl -X POST "http://localhost:5000/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "username": "testuser",
    "password": "Test@1234"
  }'
```

### 3. Get User Profile (Authenticated)

```bash
curl -X GET "http://localhost:5000/api/user/profile" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN"
```

### 4. Refresh Token

```bash
curl -X POST "http://localhost:5000/api/auth/refresh" \
  -H "Content-Type: application/json" \
  -d '{
    "refreshToken": "your_refresh_token_here"
  }'
```

## Testing

### Run Unit Tests

```bash
dotnet test tests/WeatherApi.UnitTests/WeatherApi.UnitTests.csproj
```

### Run Integration Tests

Integration tests use Testcontainers to spin up a real MongoDB instance:

```bash
dotnet test tests/WeatherApi.IntegrationTests/WeatherApi.IntegrationTests.csproj
```

### Run All Tests

```bash
dotnet test
```

## Project Structure

```
WeatherApi/
├── src/
│   ├── WeatherApi.Domain/           # Domain entities and interfaces
│   ├── WeatherApi.Application/      # Business logic and DTOs
│   ├── WeatherApi.Infrastructure/   # Data access and external services
│   └── WeatherApi.API/              # API controllers and middleware
├── tests/
│   ├── WeatherApi.UnitTests/        # Unit tests
│   └── WeatherApi.IntegrationTests/ # Integration tests with BDD
├── docker-compose.yml               # Docker Compose configuration
├── Dockerfile                       # Docker image definition
└── README.md                        # This file
```

## Configuration

### JWT Settings

Configure JWT settings in `appsettings.json`:

```json
{
  "JwtSettings": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "WeatherApi",
    "Audience": "WeatherApiUsers",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 7
  }
}
```

⚠️ **Important:** Never commit real secret keys to source control. Use environment variables or secure secret management in production.

### MongoDB Settings

Configure MongoDB connection in `appsettings.json`:

```json
{
  "MongoDbSettings": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "WeatherApiDb"
  }
}
```

## Security Best Practices

1. **JWT Secret Key**: Always use a strong, randomly generated secret key (at least 32 characters)
2. **HTTPS Only**: In production, enforce HTTPS for all endpoints
3. **CORS**: Configure CORS policy to only allow trusted origins
4. **Rate Limiting**: Consider implementing rate limiting middleware (AspNetCoreRateLimit package is already included)
5. **Password Policy**: The API enforces strong password requirements:
   - Minimum 8 characters
   - At least one uppercase letter
   - At least one lowercase letter
   - At least one number
   - At least one special character

## User Roles

- **Public** - No authentication required (limited access)
- **User** - Authenticated users with basic access
- **Premium** - Users with enhanced features (higher rate limits)
- **Admin** - Full system access

## Error Responses

The API uses a consistent error response format:

```json
{
  "success": false,
  "error": {
    "code": "ERROR_CODE",
    "message": "Human-readable error message",
    "details": [
      {
        "field": "fieldName",
        "message": "Field-specific error"
      }
    ],
    "timestamp": "2025-10-20T11:30:00Z"
  },
  "timestamp": "2025-10-20T11:30:00Z"
}
```

## Logging

The application uses Serilog for structured logging. Logs are written to:
- Console (in all environments)
- Can be extended to file, database, or external logging services

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.

## Support

For issues, questions, or contributions, please open an issue on the GitHub repository.

## Roadmap

- [ ] Weather-related endpoints (as per architecture specification)
- [ ] Rate limiting implementation
- [ ] API key authentication
- [ ] Admin endpoints for user management
- [ ] Caching layer with Redis
- [ ] External weather API integration
- [ ] Historical weather data storage
- [ ] GraphQL support (optional)

---

**Version:** 1.0.0  
**Last Updated:** October 20, 2025

