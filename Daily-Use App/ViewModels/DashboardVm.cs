using Daily_Use_App.Models;
using Daily_Use_App.Services;

namespace Daily_Use_App.ViewModels
{
    public class DashboardVm
    {
        public WeatherNow Weather { get; set; }
        public MoodEntry? TodayMood { get; set; }
        public decimal TodaySpend { get; set; }
        public IEnumerable<Expense> RecentExpenses { get; set; } = new List<Expense>();
        public IEnumerable<Note> Notes { get; set; } = new List<Note>();
        public IEnumerable<UtilityStatus> RecentUtilities { get; set; } = new List<UtilityStatus>();
    }

}
