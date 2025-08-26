namespace Daily_Use_App.Services
{
    public class DummyWeatherService : IWeatherService
    {
        public Task<WeatherNow> GetWeatherAsync(string? city = null)
        {
            return Task.FromResult(new WeatherNow("Sunny", 30.0, "sun"));
        }
    }
}
