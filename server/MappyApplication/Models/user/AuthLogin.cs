using System.ComponentModel.DataAnnotations;

namespace MappyApplication.Models.user;

public class AuthLogin
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }
}