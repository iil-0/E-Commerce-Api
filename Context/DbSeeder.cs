using ECommerce.Api.Enums;
using ECommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Context;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Zaten veri varsa ekleme
        if (await context.Users.AnyAsync())
            return;

        var now = DateTime.UtcNow;

        // Users
        var users = new List<User>
        {
            new() { FullName = "Admin User", Email = "admin@ecommerce.com", Password = "admin123", Role = UserRole.Admin, CreatedAt = now, UpdatedAt = now },
            new() { FullName = "Test User", Email = "user@ecommerce.com", Password = "user123", Role = UserRole.User, CreatedAt = now, UpdatedAt = now }
        };
        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();

        // Categories
        var categories = new List<Category>
        {
            new() { Name = "Elektronik", Description = "Telefon, bilgisayar, tablet", CreatedAt = now, UpdatedAt = now },
            new() { Name = "Giyim", Description = "Kıyafet ve aksesuarlar", CreatedAt = now, UpdatedAt = now },
            new() { Name = "Ev & Yaşam", Description = "Ev eşyaları", CreatedAt = now, UpdatedAt = now }
        };
        await context.Categories.AddRangeAsync(categories);
        await context.SaveChangesAsync();

        // Products
        var products = new List<Product>
        {
            new() { Name = "iPhone 15", Description = "Apple iPhone 15 128GB", Price = 50000, StockQuantity = 50, CategoryId = categories[0].Id, CreatedAt = now, UpdatedAt = now },
            new() { Name = "MacBook Pro", Description = "Apple MacBook Pro 14 inch", Price = 85000, StockQuantity = 20, CategoryId = categories[0].Id, CreatedAt = now, UpdatedAt = now },
            new() { Name = "Erkek Tişört", Description = "Pamuklu tişört", Price = 250, StockQuantity = 100, CategoryId = categories[1].Id, CreatedAt = now, UpdatedAt = now },
            new() { Name = "Kadın Elbise", Description = "Yazlık elbise", Price = 500, StockQuantity = 75, CategoryId = categories[1].Id, CreatedAt = now, UpdatedAt = now },
            new() { Name = "Masa Lambası", Description = "LED masa lambası", Price = 350, StockQuantity = 30, CategoryId = categories[2].Id, CreatedAt = now, UpdatedAt = now }
        };
        await context.Products.AddRangeAsync(products);
        await context.SaveChangesAsync();
    }
}
