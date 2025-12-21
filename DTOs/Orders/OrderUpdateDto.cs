using System.ComponentModel.DataAnnotations;
using ECommerce.Api.DTOs.OrderItems;
using ECommerce.Api.Enums;

namespace ECommerce.Api.DTOs.Orders;

public class OrderUpdateDto
{
    [Required]
    public List<OrderItemCreateDto> Items { get; set; } = new();

    [Required]
    public PaymentMethod PaymentMethod { get; set; }

    [Required]
    public OrderStatus Status { get; set; }
}
