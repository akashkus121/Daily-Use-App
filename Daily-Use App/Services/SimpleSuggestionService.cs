using System;

namespace Daily_Use_App.Services
{
    public class SimpleSuggestionService : ISuggestionService
    {
        private static readonly string[] NeutralQuotes = new[]
        {
            "Small steps add up. Keep going.",
            "You’re doing better than you think.",
            "Progress, not perfection.",
            "One task at a time."
        };

        private static readonly string[] CalmTips = new[]
        {
            "Take a 2-minute breathing break.",
            "Do a quick 10-minute walk.",
            "Stretch for 2 minutes.",
            "Drink a glass of water."
        };

        private static readonly string[] EnergyTips = new[]
        {
            "Tackle a small task to build momentum.",
            "Write down the top 3 priorities.",
            "Put on your favorite focus playlist.",
            "Set a 15-minute timer and start."
        };

        public Task<Suggestion> GetSuggestionAsync(int? moodScore = null)
        {
            var rnd = Random.Shared;
            if (moodScore is null)
            {
                return Task.FromResult(new Suggestion(CalmTips[rnd.Next(CalmTips.Length)], "wellness"));
            }

            if (moodScore <= 3)
            {
                return Task.FromResult(new Suggestion(CalmTips[rnd.Next(CalmTips.Length)], "calm"));
            }
            else if (moodScore <= 7)
            {
                return Task.FromResult(new Suggestion(EnergyTips[rnd.Next(EnergyTips.Length)], "focus"));
            }
            else
            {
                return Task.FromResult(new Suggestion("Keep the streak! Do one healthy action now.", "motivation"));
            }
        }

        public Task<string> GetMotivationalQuoteAsync(int? moodScore = null)
        {
            var rnd = Random.Shared;
            return Task.FromResult(NeutralQuotes[rnd.Next(NeutralQuotes.Length)]);
        }
    }
}

