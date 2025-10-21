using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;

namespace WeatherApi.Application.Commands;

/// <summary>
/// Handler for UpsertWeatherCommand.
/// </summary>
public class UpsertWeatherCommandHandler
{
    private readonly IWeatherRepository _repository;

    public UpsertWeatherCommandHandler(IWeatherRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Handles the single upsert command.
    /// </summary>
    public async Task HandleAsync(UpsertWeatherCommand command)
    {
        var entity = new WeatherDataPoint
        {
            City = command.DataPoint.City,
            Timestamp = command.DataPoint.Timestamp,
            Temperature = command.DataPoint.Temperature,
            Humidity = command.DataPoint.Humidity,
            WindSpeed = command.DataPoint.WindSpeed,
            Condition = command.DataPoint.Condition,
            LastUpdated = DateTime.UtcNow
        };

        await _repository.UpsertWeatherDataPointAsync(entity);
    }
}

