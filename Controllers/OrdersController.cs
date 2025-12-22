using ECommerce.Api.Common;
using ECommerce.Api.DTOs.Orders;
using ECommerce.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // GET: api/orders
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<OrderResponseDto>>>> GetAll()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(new ApiResponse<List<OrderResponseDto>>(true, "Siparişler başarıyla getirildi", orders));
    }

    // GET: api/orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> GetById(int id)
    {
        var order = await _orderService.GetByIdAsync(id);

        if (order == null)
            return NotFound(new ApiResponse<OrderResponseDto>(false, "Sipariş bulunamadı", null));

        return Ok(new ApiResponse<OrderResponseDto>(true, "Sipariş başarıyla getirildi", order));
    }

    // POST: api/orders
    [HttpPost]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> Create([FromBody] OrderCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<OrderResponseDto>(false, "Geçersiz veri", null));

        var created = await _orderService.CreateAsync(dto);

        if (created == null)
            return BadRequest(new ApiResponse<OrderResponseDto>(false, "Sipariş oluşturulamadı. Kullanıcı, ürün veya stok hatası olabilir.", null));

        return CreatedAtAction(nameof(GetById), new { id = created.Id },
            new ApiResponse<OrderResponseDto>(true, "Sipariş başarıyla oluşturuldu", created));
    }

    // PUT: api/orders/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> Update(int id, [FromBody] OrderUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ApiResponse<OrderResponseDto>(false, "Geçersiz veri", null));

        var updated = await _orderService.UpdateAsync(id, dto);

        if (updated == null)
            return NotFound(new ApiResponse<OrderResponseDto>(false, "Sipariş bulunamadı", null));

        return Ok(new ApiResponse<OrderResponseDto>(true, "Sipariş başarıyla güncellendi", updated));
    }

    // DELETE: api/orders/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(int id)
    {
        var result = await _orderService.DeleteAsync(id);

        if (!result)
            return NotFound(new ApiResponse<object>(false, "Sipariş bulunamadı", null));

        return Ok(new ApiResponse<object>(true, "Sipariş başarıyla silindi", null));
    }
}
