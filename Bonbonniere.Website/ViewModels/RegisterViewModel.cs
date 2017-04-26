using System.ComponentModel.DataAnnotations;

namespace Bonbonniere.Website.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be longer than 5 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
