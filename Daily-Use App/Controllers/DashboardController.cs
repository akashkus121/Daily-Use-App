using Daily_Use_App.Data;
using Daily_Use_App.Models;
using Daily_Use_App.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Daily_Use_App.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _db;

        public DashboardController(AppDbContext db)
        {
            _db = db;
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

            // Redirect to a page with links to Mood and Expense pages (Dashboard view)
            // Also preload some simple dashboard data if needed later
            var vm = new DashboardVm
            {
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
                    .ToListAsync()
            };

            return View(vm);
        }
    }
}

