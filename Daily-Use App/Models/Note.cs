using System.ComponentModel.DataAnnotations;

namespace Daily_Use_App.Models
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        [MaxLength(200)] public string? Title { get; set; }
        [Required] public string Content { get; set; } = string.Empty;
        public bool IsPinned { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
