using System.ComponentModel.DataAnnotations;

namespace Daily_Use_App.Models
{
    public class UtilityStatus
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        [Required, MaxLength(20)] public string Type { get; set; } = "Power"; // Power/Water
        [Required, MaxLength(20)] public string Status { get; set; } = "Available"; // Available/Outage/Low Pressure
        [MaxLength(200)] public string? Location { get; set; }
        public DateTime NotedAt { get; set; }
        [MaxLength(300)] public string? Note { get; set; }
    }
}
