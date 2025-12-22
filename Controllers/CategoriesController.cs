using ECommerce.Api.Common;
using ECommerce.Api.DTOs.Categories;
using ECommerce.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoriesController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET: api/categories
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<CategoryResponseDto>>>> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(new ApiResponse<List<CategoryResponseDto>>(true, "Kategoriler başarıyla getirildi", categories));
    }

    // GET: api/categories/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> GetById(int id)
    {
        var category = await _categoryService.GetByIdAsync(id);

        if (category == null)
            return NotFound(new ApiResponse<CategoryResponseDto>(false, "Kategori bulunamadı", null));

        return Ok(new ApiResponse<CategoryResponseDto>(true, "Kategori başarıyla getirildi", category));
    }

    // POST: api/categories
    [HttpPost]
    public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> Create([FromBody] CategoryCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<CategoryResponseDto>(false, "Geçersiz veri", null));

        var created = await _categoryService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id },
            new ApiResponse<CategoryResponseDto>(true, "Kategori başarıyla oluşturuldu", created));
    }

    // PUT: api/categories/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<CategoryResponseDto>>> Update(int id, [FromBody] CategoryUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<CategoryResponseDto>(false, "Geçersiz veri", null));

        var updated = await _categoryService.UpdateAsync(id, dto);

        if (updated == null)
            return NotFound(new ApiResponse<CategoryResponseDto>(false, "Kategori bulunamadı", null));

        return Ok(new ApiResponse<CategoryResponseDto>(true, "Kategori başarıyla güncellendi", updated));
    }

    // DELETE: api/categories/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var result = await _categoryService.DeleteAsync(id);

        if (!result)
            return NotFound(new ApiResponse<object>(false, "Kategori bulunamadı", null));

        return Ok(new ApiResponse<object>(true, "Kategori başarıyla silindi", null));
    }
}
