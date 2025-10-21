using System.Text.RegularExpressions;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<WeatherDataResponse>>> GetWeather(
        string city, 
        [FromQuery] int days = 5)
    {
        // Validate city parameter (fixes Issue #3)
        if (string.IsNullOrWhiteSpace(city))
        {
            return BadRequest(new { Message = "City parameter is required" });
        }

        // Sanitize city name to prevent injection (fixes CRITICAL-004)
        var sanitizedCity = Regex.Replace(city.Trim(), @"[^\w\s-]", "");
        if (sanitizedCity != city.Trim())
        {
            return BadRequest(new { Message = "City name contains invalid characters. Only letters, numbers, spaces, and hyphens are allowed." });
        }

        var query = new GetWeatherQuery { City = sanitizedCity, Days = days };
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
        // Validate request is not null (fixes CRITICAL-001)
        if (requests == null || requests.Count == 0)
        {
            return BadRequest(new { Message = "Request body cannot be empty" });
        }

        // Check for null items in the list (fixes CRITICAL-001)
        if (requests.Any(r => r == null))
        {
            return BadRequest(new { Message = "Request contains null items" });
        }

        if (requests.Count > 1000)
        {
            return BadRequest(new { Message = "Maximum 1000 items per bulk request. Please split into smaller batches." });
        }

        // Validate all requests in parallel for better performance (fixes HIGH-005)
        var validationTasks = requests.Select(r => _validator.ValidateAsync(r));
        var validationResults = await Task.WhenAll(validationTasks);

        var errors = validationResults
            .Where(r => !r.IsValid)
            .SelectMany(r => r.Errors)
            .ToList();

        if (errors.Any())
        {
            return BadRequest(new 
            { 
                Message = "Validation failed for one or more items",
                Errors = errors.Select(e => new { e.PropertyName, e.ErrorMessage })
            });
        }

        var command = new UpsertBulkWeatherCommand { DataPoints = requests };
        await _bulkUpsertHandler.HandleAsync(command);
        return NoContent();
    }
}
