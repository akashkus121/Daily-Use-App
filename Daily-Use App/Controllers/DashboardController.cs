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
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var today = DateTime.UtcNow.Date;
            var firstUser = await _db.Users.FirstAsync(u => u.Id == userId.Value);

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

