using System.ComponentModel.DataAnnotations;

namespace CrimeAnalysisSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
        
        [MaxLength(50)]
        public string Role { get; set; } = "User";
        
        [MaxLength(100)]
        public string ApiToken { get; set; } = Guid.NewGuid().ToString();
        
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }
}
