using ECommerce.Api.Enums;

namespace ECommerce.Api.DTOs.Users;

public class UserResponseDto
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string? FullName { get; set; }

    public UserRole Role { get; set; }

    public DateTime CreatedAt { get; set; }
}
