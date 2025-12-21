using ECommerce.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.DTOs.Users;

public class UserUpdateDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public string? Password { get; set; }
}
