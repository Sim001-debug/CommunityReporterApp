using System.ComponentModel.DataAnnotations;

namespace CommunityReporterApp.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User"; // or Admin?
    }
}
