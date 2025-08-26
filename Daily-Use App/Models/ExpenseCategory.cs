using System.ComponentModel.DataAnnotations;

namespace Daily_Use_App.Models
{
    public class ExpenseCategory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
        [MaxLength(50)] public string? Icon { get; set; }
    }
}
