using System.ComponentModel.DataAnnotations;

namespace EcommerceApi.V1.Users.Models;

public class RegisterModel
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [Required]
    public string PhoneNumber { get; set; }

}
