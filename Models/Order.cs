using ECommerce.Api.Enums;
using System;
using System.Collections.Generic;

namespace ECommerce.Api.Models;

public class Order
{
    public int Id { get; set; }

    // FK
    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending; // default value atadik.
    public bool IsDeleted { get; set; } = false;

    // Navigation
    public User User { get; set; } = null!;
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
