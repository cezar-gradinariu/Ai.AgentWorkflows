using FluentValidation;
using WeatherApi.Application.DTOs;

namespace WeatherApi.Application.Validators;

/// <summary>
/// Validator for weather data requests.
/// </summary>
public class WeatherDataRequestValidator : AbstractValidator<WeatherDataRequest>
{
    public WeatherDataRequestValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("City name is required")
            .MaximumLength(100)
            .WithMessage("City name cannot exceed 100 characters");

        RuleFor(x => x.Timestamp)
            .NotEmpty()
            .WithMessage("Timestamp is required");

        RuleFor(x => x.Temperature)
            .InclusiveBetween(-100, 60)
            .WithMessage("Temperature must be between -100°C and 60°C");

        RuleFor(x => x.Humidity)
            .InclusiveBetween(0, 100)
            .WithMessage("Humidity must be between 0% and 100%");

        RuleFor(x => x.WindSpeed)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Wind speed cannot be negative");

        RuleFor(x => x.Condition)
            .MaximumLength(200)
            .WithMessage("Condition description cannot exceed 200 characters");
    }
}

