using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Task_API.Models
{
    public class Login_model
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                  ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character (@$!%*?&).")]
        public string? Password { get; set; }

        [JsonIgnore]

        public string? Role { get; set; } 
    }
}
