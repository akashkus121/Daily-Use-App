namespace Daily_Use_App.Services
{
    public record Suggestion(string Text, string Category);

    public interface ISuggestionService
    {
        Task<Suggestion> GetSuggestionAsync(int? moodScore = null);
        Task<string> GetMotivationalQuoteAsync(int? moodScore = null);
    }
}

