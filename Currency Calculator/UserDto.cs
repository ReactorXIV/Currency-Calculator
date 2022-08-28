using System.ComponentModel.DataAnnotations;

namespace Currency_Calculator
{
    public record UserDto
    {
        /// <example>admin</example>
        [MinLength(1)]
        [Required]
        public string Username { get; set; } = null!;

        /// <example>admin</example>
        [MinLength(4)]
        [Required]
        public string Password { get; set; } = null!;
    }
}
