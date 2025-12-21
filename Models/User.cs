using ECommerce.Api.Enums;

namespace ECommerce.Api.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; } = UserRole.User;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // SOFT DELETE
    public bool IsDeleted { get; set; } = false;

    public ICollection<Order>? Orders { get; set; } = new List<Order>();
}
