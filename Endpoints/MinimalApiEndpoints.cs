using ECommerce.Api.Common;
using ECommerce.Api.DTOs.Categories;
using ECommerce.Api.DTOs.Products;
using ECommerce.Api.Interfaces;

namespace ECommerce.Api.Endpoints;

public static class MinimalApiEndpoints
{
    public static void MapMinimalApiEndpoints(this WebApplication app)
    {
        // Category Minimal API Endpoints
        var categoryGroup = app.MapGroup("/api/minimal/categories")
            .WithTags("Minimal API - Categories");

        categoryGroup.MapGet("/", async (ICategoryService service) =>
        {
            var categories = await service.GetAllAsync();
            return Results.Ok(new ApiResponse<List<CategoryResponseDto>>(true, "Kategoriler getirildi", categories));
        });

        categoryGroup.MapGet("/{id}", async (int id, ICategoryService service) =>
        {
            var category = await service.GetByIdAsync(id);
            if (category == null)
                return Results.NotFound(new ApiResponse<CategoryResponseDto>(false, "Kategori bulunamadı", null));
            return Results.Ok(new ApiResponse<CategoryResponseDto>(true, "Kategori getirildi", category));
        });

        categoryGroup.MapPost("/", async (CategoryCreateDto dto, ICategoryService service) =>
        {
            var created = await service.CreateAsync(dto);
            return Results.Created($"/api/minimal/categories/{created.Id}",
                new ApiResponse<CategoryResponseDto>(true, "Kategori oluşturuldu", created));
        });

        categoryGroup.MapPut("/{id}", async (int id, CategoryUpdateDto dto, ICategoryService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated == null)
                return Results.NotFound(new ApiResponse<CategoryResponseDto>(false, "Kategori bulunamadı", null));
            return Results.Ok(new ApiResponse<CategoryResponseDto>(true, "Kategori güncellendi", updated));
        });

        categoryGroup.MapDelete("/{id}", async (int id, ICategoryService service) =>
        {
            var result = await service.DeleteAsync(id);
            if (!result)
                return Results.NotFound(new ApiResponse<object>(false, "Kategori bulunamadı", null));
            return Results.Ok(new ApiResponse<object>(true, "Kategori silindi", null));
        });

        // Product Minimal API Endpoints
        var productGroup = app.MapGroup("/api/minimal/products")
            .WithTags("Minimal API - Products");

        productGroup.MapGet("/", async (IProductService service) =>
        {
            var products = await service.GetAllAsync();
            return Results.Ok(new ApiResponse<List<ProductResponseDto>>(true, "Ürünler getirildi", products));
        });

        productGroup.MapGet("/{id}", async (int id, IProductService service) =>
        {
            var product = await service.GetByIdAsync(id);
            if (product == null)
                return Results.NotFound(new ApiResponse<ProductResponseDto>(false, "Ürün bulunamadı", null));
            return Results.Ok(new ApiResponse<ProductResponseDto>(true, "Ürün getirildi", product));
        });

        productGroup.MapPost("/", async (ProductCreateDto dto, IProductService service) =>
        {
            var created = await service.CreateAsync(dto);
            if (created == null)
                return Results.BadRequest(new ApiResponse<ProductResponseDto>(false, "Geçersiz kategori", null));
            return Results.Created($"/api/minimal/products/{created.Id}",
                new ApiResponse<ProductResponseDto>(true, "Ürün oluşturuldu", created));
        });

        productGroup.MapPut("/{id}", async (int id, ProductUpdateDto dto, IProductService service) =>
        {
            var updated = await service.UpdateAsync(id, dto);
            if (updated == null)
                return Results.NotFound(new ApiResponse<ProductResponseDto>(false, "Ürün bulunamadı", null));
            return Results.Ok(new ApiResponse<ProductResponseDto>(true, "Ürün güncellendi", updated));
        });

        productGroup.MapDelete("/{id}", async (int id, IProductService service) =>
        {
            var result = await service.DeleteAsync(id);
            if (!result)
                return Results.NotFound(new ApiResponse<object>(false, "Ürün bulunamadı", null));
            return Results.Ok(new ApiResponse<object>(true, "Ürün silindi", null));
        });
    }
}
