using ECommerce.Api.Common;
using ECommerce.Api.DTOs.Products;
using ECommerce.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/products
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<ProductResponseDto>>>> GetAll()
    {
        var products = await _productService.GetAllAsync();
        return Ok(new ApiResponse<List<ProductResponseDto>>(true, "Ürünler başarıyla getirildi", products));
    }

    // GET: api/products/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
            return NotFound(new ApiResponse<ProductResponseDto>(false, "Ürün bulunamadı", null));

        return Ok(new ApiResponse<ProductResponseDto>(true, "Ürün başarıyla getirildi", product));
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Create([FromBody] ProductCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ProductResponseDto>(false, "Geçersiz veri", null));

        var created = await _productService.CreateAsync(dto);

        if (created == null)
            return BadRequest(new ApiResponse<ProductResponseDto>(false, "Geçersiz kategori ID", null));

        return CreatedAtAction(nameof(GetById), new { id = created.Id },
            new ApiResponse<ProductResponseDto>(true, "Ürün başarıyla oluşturuldu", created));
    }

    // PUT: api/products/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<ProductResponseDto>>> Update(int id, [FromBody] ProductUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<ProductResponseDto>(false, "Geçersiz veri", null));

        var updated = await _productService.UpdateAsync(id, dto);

        if (updated == null)
            return NotFound(new ApiResponse<ProductResponseDto>(false, "Ürün bulunamadı", null));

        return Ok(new ApiResponse<ProductResponseDto>(true, "Ürün başarıyla güncellendi", updated));
    }

    // DELETE: api/products/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var result = await _productService.DeleteAsync(id);

        if (!result)
            return NotFound(new ApiResponse<object>(false, "Ürün bulunamadı", null));

        return Ok(new ApiResponse<object>(true, "Ürün başarıyla silindi", null));
    }
}
