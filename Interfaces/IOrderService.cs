using ECommerce.Api.DTOs.Orders;

namespace ECommerce.Api.Interfaces;

public interface IOrderService
{
    Task<List<OrderResponseDto>> GetAllAsync();
    Task<OrderResponseDto?> GetByIdAsync(int id);
    Task<OrderResponseDto?> CreateAsync(OrderCreateDto dto);
    Task<OrderResponseDto?> UpdateAsync(int id, OrderUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
