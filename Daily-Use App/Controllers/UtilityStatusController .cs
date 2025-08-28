using Daily_Use_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Daily_Use_App.Data;

namespace Daily_Use_App.Controllers
{
    public class UtilityStatusController : Controller
    {
        private readonly AppDbContext _context;

        public UtilityStatusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UtilityStatus
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId is null) return RedirectToAction("Login", "Auth");

            var user = await _context.Users.FirstAsync(u => u.Id == userId.Value);

            IQueryable<UtilityStatus> query = _context.UtilityStatuses.Include(u => u.User);
            if (!string.IsNullOrWhiteSpace(user.Location))
            {
                query = query.Where(u => u.Location == user.Location);
            }
            var statuses = await query.ToListAsync();
            return View(statuses);
        }

        // GET: UtilityStatus/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var status = await _context.UtilityStatuses
                                       .Include(u => u.User)
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (status == null) return NotFound();

            return View(status);
        }

        // GET: UtilityStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UtilityStatus/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UtilityStatus status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(status);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: UtilityStatus/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var status = await _context.UtilityStatuses.FindAsync(id);
            if (status == null) return NotFound();

            return View(status);
        }

        // POST: UtilityStatus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UtilityStatus status)
        {
            if (id != status.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.UtilityStatuses.Any(e => e.Id == id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: UtilityStatus/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _context.UtilityStatuses
                                       .FirstOrDefaultAsync(m => m.Id == id);
            if (status == null) return NotFound();

            return View(status);
        }

        // POST: UtilityStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _context.UtilityStatuses.FindAsync(id);
            if (status != null)
            {
                _context.UtilityStatuses.Remove(status);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
