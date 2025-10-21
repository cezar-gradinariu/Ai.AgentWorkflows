using FluentValidation;
using WeatherApi.Application.DTOs;

namespace WeatherApi.Application.Validators;

/// <summary>
/// Validator for weather data requests.
/// </summary>
public class WeatherDataRequestValidator : AbstractValidator<WeatherDataRequest>
{
    // Validation constants (fixes MINOR-001)
    private const double MinTemperature = -100;
    private const double MaxTemperature = 60;
    private const double MinHumidity = 0;
    private const double MaxHumidity = 100;
    private const double MinWindSpeed = 0;
    private const int MaxCityNameLength = 100;
    private const int MaxConditionLength = 200;

    public WeatherDataRequestValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City name is required")
            .MaximumLength(MaxCityNameLength)
            .WithMessage($"City name cannot exceed {MaxCityNameLength} characters");

        RuleFor(x => x.Timestamp)
            .NotEmpty()
            .WithMessage("Timestamp is required");

        RuleFor(x => x.Temperature)
            .InclusiveBetween(MinTemperature, MaxTemperature)
            .WithMessage($"Temperature must be between {MinTemperature}°C and {MaxTemperature}°C");

        RuleFor(x => x.Humidity)
            .InclusiveBetween(MinHumidity, MaxHumidity)
            .WithMessage($"Humidity must be between {MinHumidity}% and {MaxHumidity}%");

        RuleFor(x => x.WindSpeed)
            .GreaterThanOrEqualTo(MinWindSpeed)
            .WithMessage("Wind speed cannot be negative");

        RuleFor(x => x.Condition)
            .MaximumLength(MaxConditionLength)
            .WithMessage($"Condition description cannot exceed {MaxConditionLength} characters");
    }
}
