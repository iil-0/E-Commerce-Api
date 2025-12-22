using ECommerce.Api.Context;
using ECommerce.Api.DTOs.Categories;
using ECommerce.Api.Interfaces;
using ECommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    // Tüm kategorileri listele
    public async Task<List<CategoryResponseDto>> GetAllAsync()
    {
        var categories = await _context.Categories
            .Where(c => !c.IsDeleted)
            .ToListAsync();

        return categories.Select(MapToResponse).ToList();
    }

    // Id'ye göre tek kategori getir
    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if (category == null)
            return null;

        return MapToResponse(category);
    }

    // Yeni kategori oluştur
    public async Task<CategoryResponseDto> CreateAsync(CategoryCreateDto dto)
    {
        var now = DateTime.UtcNow;

        var category = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = now,
            UpdatedAt = now,
            IsDeleted = false
        };

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return MapToResponse(category);
    }

    // Var olan kategoriyi güncelle
    public async Task<CategoryResponseDto?> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if (category == null)
            return null;

        category.Name = dto.Name;
        category.Description = dto.Description;
        category.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToResponse(category);
    }

    // Soft delete (IsDeleted = true)
    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if (category == null)
            return false;

        category.IsDeleted = true;
        category.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    // Entity -> Response DTO map
    private static CategoryResponseDto MapToResponse(Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            CreatedAt = category.CreatedAt,
        };
    }
}
