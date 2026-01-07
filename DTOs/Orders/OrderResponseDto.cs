using ECommerce.Api.DTOs.OrderItems;
using ECommerce.Api.Enums;

namespace ECommerce.Api.DTOs.Orders;

public class OrderResponseDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemResponseDto> Items { get; set; } = new();

    public DateTime CreatedAt { get; set; }
}
