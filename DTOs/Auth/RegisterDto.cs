using ECommerce.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.DTOs.Auth;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public UserRole Role { get; set; } = UserRole.User;
}
