using System;
using System.Collections.Generic;

namespace ECommerce.Api.Models;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    // 1 Category -> N Products
    public ICollection<Product> Products { get; set; } = new List<Product>();    
}
