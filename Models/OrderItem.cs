using System;

namespace ECommerce.Api.Models;

public class OrderItem
{
    public int Id { get; set; }

    // FK'ler
    public int OrderId { get; set; }
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Navigation
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
}
