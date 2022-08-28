using System.ComponentModel.DataAnnotations;

namespace Currency_Calculator.Models
{
    public record User
    {
        [MinLength(1)]
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public byte[] PasswordHash { get; set; } = null!;
        [Required]
        public byte[] PasswordSalt { get; set; } = null!;
    }
}
