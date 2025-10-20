using System.Security.Claims;
using WeatherApi.Domain.Enums;

namespace WeatherApi.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(string userId, string username, string email, UserRole role);
    string GenerateRefreshToken();
    ClaimsPrincipal? ValidateToken(string token);
}

