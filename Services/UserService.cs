using ECommerce.Api.Context;
using ECommerce.Api.DTOs.Users;
using ECommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Tüm kullanıcıları listele
    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        var users = await _context.Users
            .Where(u => !u.IsDeleted)
            .ToListAsync();

        return users.Select(MapToResponse).ToList();
    }

    // Id'ye göre kullanıcı
    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        if (user == null)
            return null;

        return MapToResponse(user);
    }

    // Yeni kullanıcı oluştur
    public async Task<UserResponseDto?> CreateAsync(UserCreateDto dto)
    {
        var exists = await _context.Users
            .AnyAsync(u => u.Email == dto.Email && !u.IsDeleted);

        if (exists)
            return null;

        var now = DateTime.UtcNow;

        var user = new User
        {
            FullName = dto.FullName ?? string.Empty,
            Email = dto.Email,
            Password = dto.Password,
            Role = dto.Role,
            CreatedAt = now,
            UpdatedAt = now,
            IsDeleted = false
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return MapToResponse(user);
    }

    // Kullanıcı güncelle
    public async Task<UserResponseDto?> UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        if (user == null)
            return null;

        user.Email = dto.Email;
        user.FullName = dto.FullName ?? user.FullName;
        user.Role = dto.Role;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.Password = dto.Password;
        }

        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToResponse(user);
    }

    // Soft delete
    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        if (user == null)
            return false;

        user.IsDeleted = true;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    private static UserResponseDto MapToResponse(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}
