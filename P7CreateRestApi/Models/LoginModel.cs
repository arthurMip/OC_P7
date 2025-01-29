using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")]
        public string Password { get; set; } = null!;
    }
}
