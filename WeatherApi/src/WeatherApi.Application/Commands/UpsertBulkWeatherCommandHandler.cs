using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;

namespace WeatherApi.Application.Commands;

/// <summary>
/// Handler for UpsertBulkWeatherCommand.
/// </summary>
public class UpsertBulkWeatherCommandHandler
{
    private readonly IWeatherRepository _repository;

    public UpsertBulkWeatherCommandHandler(IWeatherRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the bulk upsert command.
    /// </summary>
    public async Task HandleAsync(UpsertBulkWeatherCommand command)
    {
        var entities = command.DataPoints.Select(dto => new WeatherDataPoint
        {
            City = dto.City,
            Timestamp = dto.Timestamp,
            Temperature = dto.Temperature,
            Humidity = dto.Humidity,
            WindSpeed = dto.WindSpeed,
            Condition = dto.Condition,
            LastUpdated = DateTime.UtcNow
        }).ToList();

        await _repository.UpsertWeatherDataPointsAsync(entities);
    }
}

