using System.ComponentModel.DataAnnotations;

namespace Daily_Use_App.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? CategoryId { get; set; }
        public ExpenseCategory? Category { get; set; }
        [Range(0.01, 1000000)] public decimal Amount { get; set; }
        [MaxLength(300)] public string? Description { get; set; }
        [DataType(DataType.Date)] public DateTime SpentOn { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
