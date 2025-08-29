using Daily_Use_App.Data;
using Daily_Use_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Daily_Use_App.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string? location)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null || user.PasswordHash != password)
            {
                ModelState.AddModelError(string.Empty, "Invalid credentials");
                return View();
            }

            if (string.IsNullOrWhiteSpace(user.Location) && !string.IsNullOrWhiteSpace(location))
            {
                user.Location = location;
                await _db.SaveChangesAsync();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string username, string password, string location)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ModelState.AddModelError(string.Empty, "Username and password are required");
                return View();
            }

            if (await _db.Users.AnyAsync(u => u.Username == username))
            {
                ModelState.AddModelError(string.Empty, "Username already exists");
                return View();
            }

            var user = new User
            {
                Username = username,
                PasswordHash = password,
                Location = string.IsNullOrWhiteSpace(location) ? null : location
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet]
        public IActionResult SetLocation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetLocation(string location)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null) return RedirectToAction("Login");

            var user = await _db.Users.FindAsync(userId.Value);
            if (user == null) return RedirectToAction("Login");

            user.Location = string.IsNullOrWhiteSpace(location) ? null : location;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Dashboard");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

