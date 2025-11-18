using System.ComponentModel.DataAnnotations;

namespace MappyApplication.Models.user;

public class AuthRegister
{
    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }

    [Required] public string PhoneNumber { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [Required]
    public string ConfirmPassword { get; set; }
}