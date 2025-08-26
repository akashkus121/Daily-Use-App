using System.ComponentModel.DataAnnotations;

namespace Daily_Use_App.Models
{
    public class MoodEntry
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        [Range(1, 5)] public byte Score { get; set; }
        [MaxLength(300)] public string? Note { get; set; }
        [DataType(DataType.Date)] public DateTime CheckedAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
