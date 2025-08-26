using Daily_Use_App.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Daily_Use_App.Data
{
    
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


            public DbSet<User> Users => Set<User>();
            public DbSet<Note> Notes => Set<Note>();
            public DbSet<Expense> Expenses => Set<Expense>();
            public DbSet<ExpenseCategory> ExpenseCategories => Set<ExpenseCategory>();
            public DbSet<MoodEntry> MoodEntries => Set<MoodEntry>();
            public DbSet<UtilityStatus> UtilityStatuses => Set<UtilityStatus>();


            protected override void OnModelCreating(ModelBuilder b)
            {
                base.OnModelCreating(b);
                b.Entity<MoodEntry>()
                .HasIndex(m => new { m.UserId, m.CheckedAt })
                .IsUnique();
            }
        }
    
}
