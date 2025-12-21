using ECommerce.Api.DTOs.OrderItems;
using ECommerce.Api.Enums;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.DTOs.Orders;

public class OrderCreateDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public PaymentMethod PaymentMethod { get; set; }

    [Required]
    public List<OrderItemCreateDto> Items { get; set; } = new();
}
