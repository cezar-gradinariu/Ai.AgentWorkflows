using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Application.DTOs.Responses;
using WeatherApi.Application.Services.Interfaces;

namespace WeatherApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("profile")]
    public async Task<ActionResult<ApiResponse<UserProfileResponse>>> GetProfile()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var profile = await _userService.GetProfileAsync(userId);
        if (profile == null)
        {
            return NotFound(ApiResponse<UserProfileResponse>.FailureResponse(new ErrorResponse
            {
                Code = "USER_NOT_FOUND",
                Message = "User profile not found"
            }));
        }

        return Ok(ApiResponse<UserProfileResponse>.SuccessResponse(profile));
    }

    [HttpPut("profile/email")]
    public async Task<ActionResult<ApiResponse<bool>>> UpdateEmail([FromBody] string email)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var result = await _userService.UpdateProfileAsync(userId, email);
        return Ok(ApiResponse<bool>.SuccessResponse(result));
    }

    [HttpDelete("deactivate")]
    public async Task<ActionResult<ApiResponse<bool>>> DeactivateAccount()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        var result = await _userService.DeactivateUserAsync(userId);
        return Ok(ApiResponse<bool>.SuccessResponse(result));
    }
}

