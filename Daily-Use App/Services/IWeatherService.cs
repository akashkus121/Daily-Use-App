namespace Daily_Use_App.Services
{
    public record WeatherNow(string Summary, double TempC, string Icon);


    public interface IWeatherService
    {
        Task<WeatherNow> GetWeatherAsync(string? city = null);
    }
}
