using WeatherApi.Application.DTOs.Requests;
using WeatherApi.Application.DTOs.Responses;

namespace WeatherApi.Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RefreshTokenAsync(string refreshToken);
    Task<bool> RevokeTokenAsync(string refreshToken);
    Task<bool> LogoutAsync(string userId);
}

