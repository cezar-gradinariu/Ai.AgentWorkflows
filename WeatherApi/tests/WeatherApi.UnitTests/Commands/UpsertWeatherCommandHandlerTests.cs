using FluentAssertions;
using Moq;
using WeatherApi.Application.Commands;
using WeatherApi.Application.DTOs;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;
using Xunit;

namespace WeatherApi.UnitTests.Commands;

/// <summary>
/// Unit tests for UpsertWeatherCommandHandler.
/// </summary>
public class UpsertWeatherCommandHandlerTests
{
    private readonly Mock<IWeatherRepository> _repositoryMock;
    private readonly UpsertWeatherCommandHandler _handler;

    public UpsertWeatherCommandHandlerTests()
    {
        _repositoryMock = new Mock<IWeatherRepository>();
        _handler = new UpsertWeatherCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldCallRepository_WithCorrectData()
    {
        // Arrange
        var timestamp = DateTime.UtcNow;
        var request = new WeatherDataRequest
        {
            City = "Tokyo",
            Timestamp = timestamp,
            Temperature = 25.0,
            Humidity = 80,
            WindSpeed = 20,
            Condition = "Rainy"
        };
        var command = new UpsertWeatherCommand { DataPoint = request };

        _repositoryMock.Setup(r => r.UpsertWeatherDataPointAsync(It.IsAny<WeatherDataPoint>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.HandleAsync(command);

        // Assert
        _repositoryMock.Verify(r => r.UpsertWeatherDataPointAsync(
            It.Is<WeatherDataPoint>(dp => 
                dp.City == "Tokyo" &&
                dp.Timestamp == timestamp &&
                dp.Temperature == 25.0 &&
                dp.Humidity == 80 &&
                dp.WindSpeed == 20 &&
                dp.Condition == "Rainy")), 
            Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldSetLastUpdated_ToCurrentTime()
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = "Sydney",
            Timestamp = DateTime.UtcNow.AddHours(5),
            Temperature = 22.0,
            Humidity = 60,
            WindSpeed = 15,
            Condition = "Clear"
        };
        var command = new UpsertWeatherCommand { DataPoint = request };
        var beforeTime = DateTime.UtcNow;

        _repositoryMock.Setup(r => r.UpsertWeatherDataPointAsync(It.IsAny<WeatherDataPoint>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.HandleAsync(command);
        var afterTime = DateTime.UtcNow;

        // Assert
        _repositoryMock.Verify(r => r.UpsertWeatherDataPointAsync(
            It.Is<WeatherDataPoint>(dp => 
                dp.LastUpdated >= beforeTime &&
                dp.LastUpdated <= afterTime)), 
            Times.Once);
    }
}

