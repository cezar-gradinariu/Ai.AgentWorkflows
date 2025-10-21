using Microsoft.Extensions.Caching.Memory;
using Moq;
using WeatherApi.Application.Commands;
using WeatherApi.Application.DTOs;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;
using Xunit;

namespace WeatherApi.UnitTests.Commands;

/// <summary>
/// Unit tests for UpsertBulkWeatherCommandHandler.
/// </summary>
public class UpsertBulkWeatherCommandHandlerTests
{
    private readonly Mock<IWeatherRepository> _repositoryMock;
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly UpsertBulkWeatherCommandHandler _handler;

    public UpsertBulkWeatherCommandHandlerTests()
    {
        _repositoryMock = new Mock<IWeatherRepository>();
        _cacheMock = new Mock<IMemoryCache>();
        _handler = new UpsertBulkWeatherCommandHandler(_repositoryMock.Object, _cacheMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldCallRepository_WithAllDataPoints()
    {
        // Arrange
        var requests = new List<WeatherDataRequest>
        {
            new WeatherDataRequest
            {
                City = "London",
                Timestamp = DateTime.UtcNow,
                Temperature = 15.0,
                Humidity = 70,
                WindSpeed = 10,
                Condition = "Cloudy"
            },
            new WeatherDataRequest
            {
                City = "Paris",
                Timestamp = DateTime.UtcNow.AddHours(1),
                Temperature = 18.0,
                Humidity = 65,
                WindSpeed = 12,
                Condition = "Sunny"
            }
        };
        var command = new UpsertBulkWeatherCommand { DataPoints = requests };

        _repositoryMock.Setup(r => r.UpsertWeatherDataPointsAsync(It.IsAny<IEnumerable<WeatherDataPoint>>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _repositoryMock.Verify(r => r.UpsertWeatherDataPointsAsync(
            It.Is<IEnumerable<WeatherDataPoint>>(list => list.Count() == 2)), 
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldMapAllProperties_Correctly()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var requests = new List<WeatherDataRequest>
        {
            new WeatherDataRequest
            {
                City = "Berlin",
                Timestamp = timestamp,
                Temperature = 20.0,
                Humidity = 55,
                WindSpeed = 18,
                Condition = "Windy"
            }
        };
        var command = new UpsertBulkWeatherCommand { DataPoints = requests };

        _repositoryMock.Setup(r => r.UpsertWeatherDataPointsAsync(It.IsAny<IEnumerable<WeatherDataPoint>>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _repositoryMock.Verify(r => r.UpsertWeatherDataPointsAsync(
            It.Is<IEnumerable<WeatherDataPoint>>(list => 
                list.First().City == "Berlin" &&
                list.First().Timestamp == timestamp &&
                list.First().Temperature == 20.0 &&
                list.First().Humidity == 55 &&
                list.First().WindSpeed == 18 &&
                list.First().Condition == "Windy")), 
            Times.Once);
    }
}
