# Security Configuration Guide

## API Key Configuration

### Development Environment
The development environment uses a local API key configured in `appsettings.Development.json`.

### Production Environment
**IMPORTANT:** Never commit production API keys to source control.

Configure the API key using one of these methods:

#### Option 1: Environment Variable (Recommended)
```bash
# Windows
set WEATHER_API_KEY=your-secure-api-key-here

# Linux/Mac
export WEATHER_API_KEY=your-secure-api-key-here
```

#### Option 2: Azure App Service Configuration
1. Navigate to your App Service in Azure Portal
2. Go to Configuration > Application Settings
3. Add new setting: `WEATHER_API_KEY` with your secure key

#### Option 3: Azure Key Vault (Most Secure)
```csharp
// Add to Program.cs
builder.Configuration.AddAzureKeyVault(
    new Uri($"https://{keyVaultName}.vault.azure.net/"),
    new DefaultAzureCredential());
```

## Rate Limiting

Current rate limits:
- GET endpoints: 100 requests per minute per IP
- POST /api/weather: 10 requests per minute per IP
- POST /api/weather/bulk: 5 requests per minute per IP

To customize, modify `IpRateLimitOptions` in `Program.cs`.

## MongoDB Security

1. Use connection strings with authentication
2. Enable TLS/SSL for production
3. Use separate read-only credentials for read operations
4. Regularly rotate credentials

## Health Checks

Health check endpoint: `/health`
- Returns 200 OK if MongoDB is accessible
- Returns 503 Service Unavailable if unhealthy

## Logging

Logs are written to:
- Console (all environments)
- File: `logs/weatherapi-YYYYMMDD.log` (rolling daily)

**Production:** Configure centralized logging (Application Insights, Seq, etc.)

## Input Validation

All inputs are validated:
- City names: alphanumeric, spaces, and hyphens only
- Temperature: -100°C to 60°C
- Humidity: 0% to 100%
- Wind speed: >= 0 km/h
- Bulk requests: max 1000 items

## HTTPS Enforcement

HTTPS redirection is enabled by default. Ensure certificates are properly configured in production.

