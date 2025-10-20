using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow,
            version = "1.0.0"
        });
    }

    [HttpGet("ready")]
    public IActionResult Ready()
    {
        return Ok(new
        {
            status = "Ready",
            timestamp = DateTime.UtcNow
        });
    }

    [HttpGet("live")]
    public IActionResult Live()
    {
        return Ok(new
        {
            status = "Live",
            timestamp = DateTime.UtcNow
        });
    }
}

