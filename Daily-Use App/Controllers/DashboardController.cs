using Daily_Use_App.Data;
using Daily_Use_App.Models;
using Daily_Use_App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Daily_Use_App.Services;

namespace Daily_Use_App.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWeatherService _weather;
        private readonly ISuggestionService _suggestions;

        public DashboardController(AppDbContext db, IWeatherService weather, ISuggestionService suggestions)
        {
            _db = db;
            _weather = weather;
            _suggestions = suggestions;
        }

        public async Task<IActionResult> Index()
        {
            // Ensure at least one user exists. If none, create a default user.
            if (!await _db.Users.AnyAsync())
            {
                var defaultUser = new User
                {
                    Username = "default",
                    PasswordHash = "",
                    Role = "User"
                };
                _db.Users.Add(defaultUser);
                await _db.SaveChangesAsync();
            }

            var today = DateTime.UtcNow.Date;
            var firstUser = await _db.Users.OrderBy(u => u.Id).FirstAsync();

            var todayMood = await _db.MoodEntries
                .Where(m => m.UserId == firstUser.Id && m.CheckedAt.Date == today)
                .OrderByDescending(m => m.CheckedAt)
                .FirstOrDefaultAsync();

            var todaySpend = await _db.Expenses
                .Where(e => e.UserId == firstUser.Id && e.SpentOn.Date == today)
                .SumAsync(e => (decimal?)e.Amount) ?? 0m;

            var weatherNow = await _weather.GetWeatherAsync();
            var suggestion = await _suggestions.GetSuggestionAsync(todayMood?.Score);
            var quote = await _suggestions.GetMotivationalQuoteAsync(todayMood?.Score);

            var vm = new DashboardVm
            {
                Weather = weatherNow,
                TodayMood = todayMood,
                TodaySpend = todaySpend,
                RecentExpenses = await _db.Expenses
                    .OrderByDescending(e => e.SpentOn)
                    .Take(5)
                    .ToListAsync(),
                Notes = await _db.Notes
                    .OrderByDescending(n => n.Id)
                    .Take(5)
                    .ToListAsync(),
                RecentUtilities = await _db.UtilityStatuses
                    .OrderByDescending(u => u.Id)
                    .Take(5)
                    .ToListAsync(),
                MotivationalQuote = quote,
                Suggestion = suggestion
            };

            return View(vm);
        }
    }
}

