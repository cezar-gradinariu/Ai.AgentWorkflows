using Microsoft.AspNetCore.Mvc;
using WeatherApi.Application.Commands;
using WeatherApi.Application.DTOs;
using WeatherApi.Application.Queries;
using WeatherApi.Application.Validators;

namespace WeatherApi.Api.Controllers;

/// <summary>
/// Weather API controller for managing weather data.
/// </summary>
[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly GetWeatherQueryHandler _queryHandler;
    private readonly UpsertWeatherCommandHandler _upsertHandler;
    private readonly UpsertBulkWeatherCommandHandler _bulkUpsertHandler;
    private readonly WeatherDataRequestValidator _validator;

    public WeatherController(
        GetWeatherQueryHandler queryHandler,
        UpsertWeatherCommandHandler upsertHandler,
        UpsertBulkWeatherCommandHandler bulkUpsertHandler,
        WeatherDataRequestValidator validator)
    {
        _queryHandler = queryHandler;
        _upsertHandler = upsertHandler;
        _bulkUpsertHandler = bulkUpsertHandler;
        _validator = validator;
    }

    /// <summary>
    /// Gets weather data for a specific city.
    /// </summary>
    /// <param name="city">The city name.</param>
    /// <param name="days">Number of days to retrieve (1-5, default: 5).</param>
    /// <returns>List of weather data points.</returns>
    [HttpGet("{city}")]
    [ProducesResponseType(typeof(List<WeatherDataResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<WeatherDataResponse>>> GetWeather(
        string city, 
        [FromQuery] int days = 5)
    {
        var query = new GetWeatherQuery { City = city, Days = days };
        var result = await _queryHandler.HandleAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// Upserts a single weather data point.
    /// Requires API key authentication.
    /// </summary>
    /// <param name="request">The weather data to upsert.</param>
    /// <returns>No content on success.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpsertWeatherData([FromBody] WeatherDataRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var command = new UpsertWeatherCommand { DataPoint = request };
        await _upsertHandler.HandleAsync(command);
        return NoContent();
    }

    /// <summary>
    /// Upserts multiple weather data points in bulk.
    /// Requires API key authentication.
    /// </summary>
    /// <param name="requests">The list of weather data to upsert.</param>
    /// <returns>No content on success.</returns>
    [HttpPost("bulk")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpsertBulkWeatherData([FromBody] List<WeatherDataRequest> requests)
    {
        // Validate all requests
        foreach (var request in requests)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new 
                { 
                    Message = "Validation failed for one or more items",
                    Errors = validationResult.Errors 
                });
            }
        }

        var command = new UpsertBulkWeatherCommand { DataPoints = requests };
        await _bulkUpsertHandler.HandleAsync(command);
        return NoContent();
    }
}

