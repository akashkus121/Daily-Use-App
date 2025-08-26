using System.ComponentModel.DataAnnotations;

namespace Daily_Use_App.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required, MaxLength(100)] public string Username { get; set; } = string.Empty;
        [Required, MaxLength(256)] public string PasswordHash { get; set; } = string.Empty;
        [Required, MaxLength(20)] public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
