namespace WeatherApi.Application.DTOs.Responses;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public ErrorResponse? Error { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public static ApiResponse<T> SuccessResponse(T data)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Timestamp = DateTime.UtcNow
        };
    }

    public static ApiResponse<T> FailureResponse(ErrorResponse error)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Error = error,
            Timestamp = DateTime.UtcNow
        };
    }
}

