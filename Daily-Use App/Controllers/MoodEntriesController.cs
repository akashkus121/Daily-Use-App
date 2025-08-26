using Daily_Use_App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Daily_Use_App.Data;

namespace Daily_Use_App.Controllers
{
    public class MoodEntriesController : Controller
    {
        private readonly AppDbContext _context;

        public MoodEntriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MoodEntries
        public async Task<IActionResult> Index()
        {
            var moods = _context.MoodEntries.Include(m => m.User);
            return View(await moods.ToListAsync());
        }

        // GET: MoodEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var mood = await _context.MoodEntries
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mood == null) return NotFound();

            return View(mood);
        }

        // GET: MoodEntries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoodEntries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Score,Note,CheckedAt")] MoodEntry moodEntry)
        {
            if (ModelState.IsValid)
            {
                moodEntry.CreatedAt = DateTime.UtcNow;
                _context.Add(moodEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moodEntry);
        }

        // GET: MoodEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var moodEntry = await _context.MoodEntries.FindAsync(id);
            if (moodEntry == null) return NotFound();

            return View(moodEntry);
        }

        // POST: MoodEntries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Score,Note,CheckedAt,CreatedAt")] MoodEntry moodEntry)
        {
            if (id != moodEntry.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moodEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoodEntryExists(moodEntry.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(moodEntry);
        }

        // GET: MoodEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var mood = await _context.MoodEntries
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mood == null) return NotFound();

            return View(mood);
        }

        // POST: MoodEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mood = await _context.MoodEntries.FindAsync(id);
            if (mood != null)
            {
                _context.MoodEntries.Remove(mood);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MoodEntryExists(int id)
        {
            return _context.MoodEntries.Any(e => e.Id == id);
        }
    }
}
