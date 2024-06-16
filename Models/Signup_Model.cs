using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Task_API.Models
{
    public class Signup_Model
    {
        [Key]
        [JsonIgnore]

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Contact is required")]
        public string? Contact { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
          ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character (@$!%*?&).")]
        public string? Password { get; set; }

        [JsonIgnore]

        public string Role { get; set; } = "Employee";

        public byte[]? ProfilePicture { get; set; }

    }
}
