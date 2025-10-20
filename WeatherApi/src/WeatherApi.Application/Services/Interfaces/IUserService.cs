using WeatherApi.Application.DTOs.Responses;

namespace WeatherApi.Application.Services.Interfaces;

public interface IUserService
{
    Task<UserProfileResponse?> GetProfileAsync(string userId);
    Task<bool> UpdateProfileAsync(string userId, string email);
    Task<bool> DeactivateUserAsync(string userId);
}

