using Microsoft.Extensions.Caching.Memory;
using Moq;
using WeatherApi.Application.Queries;
using WeatherApi.Domain.Entities;
using WeatherApi.Domain.Repositories;
using Xunit;

namespace WeatherApi.UnitTests.Queries;

/// <summary>
/// Unit tests for GetWeatherQueryHandler.
/// </summary>
public class GetWeatherQueryHandlerTests
{
    private readonly Mock<IWeatherRepository> _repositoryMock;
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly GetWeatherQueryHandler _handler;

    public GetWeatherQueryHandlerTests()
    {
        _repositoryMock = new Mock<IWeatherRepository>();
        _cacheMock = new Mock<IMemoryCache>();
        _handler = new GetWeatherQueryHandler(_repositoryMock.Object, _cacheMock.Object);
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnWeatherData_WhenDataExists()
    {
        // Arrange
        var city = "London";
        var query = new GetWeatherQuery { City = city, Days = 2 };
        var weatherData = new List<WeatherDataPoint>
        {
            new WeatherDataPoint
            {
                City = city,
                Timestamp = DateTime.UtcNow,
                Temperature = 20.5,
                Humidity = 65,
                WindSpeed = 15,
                Condition = "Sunny"
            }
        };

        // Mock cache miss
        object? cachedValue = null;
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out cachedValue))
            .Returns(false);

        _cacheMock.Setup(c => c.CreateEntry(It.IsAny<object>()))
            .Returns(Mock.Of<ICacheEntry>());

        _repositoryMock.Setup(r => r.GetWeatherAsync(
            It.IsAny<string>(),
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>()))
            .ReturnsAsync(weatherData);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(city, result[0].City);
        Assert.Equal(20.5, result[0].Temperature);
        _repositoryMock.Verify(r => r.GetWeatherAsync(
            It.IsAny<string>(),
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>()), Times.Once);
    }

    [Fact]
    public async Task HandleAsync_ShouldUseCachedData_WhenDataIsCached()
    {
        // Arrange
        var city = "Paris";
        var query = new GetWeatherQuery { City = city, Days = 1 };
        
        var cachedData = new List<WeatherApi.Application.DTOs.WeatherDataResponse>
        {
            new WeatherApi.Application.DTOs.WeatherDataResponse
            {
                City = city,
                Timestamp = DateTime.UtcNow,
                Temperature = 18.0,
                Humidity = 70,
                WindSpeed = 10,
                Condition = "Cloudy"
            }
        };

        // Mock cache hit
        object? cachedValue = cachedData;
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out cachedValue))
            .Returns(true);

        // Act
        var result = await _handler.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(city, result[0].City);
        // Repository should NOT be called due to caching
        _repositoryMock.Verify(r => r.GetWeatherAsync(
            It.IsAny<string>(),
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>()), Times.Never);
    }

    [Theory]
    [InlineData(0, 1)] // Days less than 1 should be clamped to 1
    [InlineData(6, 5)] // Days more than 5 should be clamped to 5
    [InlineData(3, 3)] // Valid days should remain unchanged
    public async Task HandleAsync_ShouldClampDaysTo1And5(int inputDays, int expectedDays)
    {
        // Arrange
        var city = "Berlin";
        var query = new GetWeatherQuery { City = city, Days = inputDays };
        
        // Mock cache miss
        object? cachedValue = null;
        _cacheMock.Setup(c => c.TryGetValue(It.IsAny<object>(), out cachedValue))
            .Returns(false);

        _cacheMock.Setup(c => c.CreateEntry(It.IsAny<object>()))
            .Returns(Mock.Of<ICacheEntry>());

        _repositoryMock.Setup(r => r.GetWeatherAsync(
            It.IsAny<string>(),
            It.IsAny<DateTime>(),
            It.IsAny<DateTime>()))
            .ReturnsAsync(new List<WeatherDataPoint>());

        // Act
        await _handler.HandleAsync(query);

        // Assert
        _repositoryMock.Verify(r => r.GetWeatherAsync(
            city,
            It.IsAny<DateTime>(),
            It.Is<DateTime>(to => to >= DateTime.UtcNow.AddDays(expectedDays - 0.1) && 
                                   to <= DateTime.UtcNow.AddDays(expectedDays + 0.1))), 
            Times.Once);
    }
}
