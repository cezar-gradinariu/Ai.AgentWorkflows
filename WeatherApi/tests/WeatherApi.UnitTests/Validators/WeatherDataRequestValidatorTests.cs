using FluentAssertions;
using WeatherApi.Application.DTOs;
using WeatherApi.Application.Validators;
using Xunit;

namespace WeatherApi.UnitTests.Validators;

/// <summary>
/// Unit tests for WeatherDataRequestValidator.
/// </summary>
public class WeatherDataRequestValidatorTests
{
    private readonly WeatherDataRequestValidator _validator;

    public WeatherDataRequestValidatorTests()
    {
        _validator = new WeatherDataRequestValidator();
    }

    [Fact]
    public async Task Validate_ShouldPass_WhenAllFieldsAreValid()
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = "London",
            Timestamp = DateTime.UtcNow,
            Temperature = 20.0,
            Humidity = 65,
            WindSpeed = 15,
            Condition = "Sunny"
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeTrue();
        result.Errors.Should().BeEmpty();
    }

    [Theory]
    [InlineData("")]
    public async Task Validate_ShouldFail_WhenCityIsEmpty(string city)
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = city,
            Timestamp = DateTime.UtcNow,
            Temperature = 20.0,
            Humidity = 65,
            WindSpeed = 15,
            Condition = "Sunny"
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "City");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenCityIsNull()
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = null!,
            Timestamp = DateTime.UtcNow,
            Temperature = 20.0,
            Humidity = 65,
            WindSpeed = 15,
            Condition = "Sunny"
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "City");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenCityExceedsMaxLength()
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = new string('A', 101), // 101 characters
            Timestamp = DateTime.UtcNow,
            Temperature = 20.0,
            Humidity = 65,
            WindSpeed = 15,
            Condition = "Sunny"
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "City" && 
            e.ErrorMessage.Contains("100 characters"));
    }

    [Theory]
    [InlineData(-101)]
    [InlineData(61)]
    public async Task Validate_ShouldFail_WhenTemperatureIsOutOfRange(double temperature)
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = "London",
            Timestamp = DateTime.UtcNow,
            Temperature = temperature,
            Humidity = 65,
            WindSpeed = 15,
            Condition = "Sunny"
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Temperature");
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public async Task Validate_ShouldFail_WhenHumidityIsOutOfRange(double humidity)
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = "London",
            Timestamp = DateTime.UtcNow,
            Temperature = 20.0,
            Humidity = humidity,
            WindSpeed = 15,
            Condition = "Sunny"
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Humidity");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenWindSpeedIsNegative()
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = "London",
            Timestamp = DateTime.UtcNow,
            Temperature = 20.0,
            Humidity = 65,
            WindSpeed = -5,
            Condition = "Sunny"
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "WindSpeed");
    }

    [Fact]
    public async Task Validate_ShouldFail_WhenConditionExceedsMaxLength()
    {
        // Arrange
        var request = new WeatherDataRequest
        {
            City = "London",
            Timestamp = DateTime.UtcNow,
            Temperature = 20.0,
            Humidity = 65,
            WindSpeed = 15,
            Condition = new string('A', 201) // 201 characters
        };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Condition" && 
            e.ErrorMessage.Contains("200 characters"));
    }
}
