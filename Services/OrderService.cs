using ECommerce.Api.Context;
using ECommerce.Api.DTOs.Orders;
using ECommerce.Api.DTOs.OrderItems;
using ECommerce.Api.Enums;
using ECommerce.Api.Interfaces;
using ECommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    // Tüm siparişleri listele
    public async Task<List<OrderResponseDto>> GetAllAsync()
    {
        var orders = await _context.Orders
            .Where(o => !o.IsDeleted)
            .Include(o => o.User)
            .Include(o => o.OrderItems)!.ThenInclude(oi => oi.Product)
            .ToListAsync();

        return orders.Select(MapToResponse).ToList();
    }

    // Id'ye göre tek sipariş
    public async Task<OrderResponseDto?> GetByIdAsync(int id)
    {
        var order = await _context.Orders
            .Where(o => !o.IsDeleted)
            .Include(o => o.User)
            .Include(o => o.OrderItems)!.ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return null;

        return MapToResponse(order);
    }

    // Sipariş oluştur
    public async Task<OrderResponseDto?> CreateAsync(OrderCreateDto dto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == dto.UserId && !u.IsDeleted);

        if (user == null)
            return null;

        if (dto.Items == null || !dto.Items.Any())
            return null;

        var now = DateTime.UtcNow;

        var order = new Order
        {
            UserId = dto.UserId,
            OrderDate = now,
            CreatedAt = now,
            UpdatedAt = now,
            PaymentMethod = dto.PaymentMethod,
            Status = OrderStatus.Pending,
            TotalAmount = 0m,
            IsDeleted = false,
            OrderItems = new List<OrderItem>()
        };

        decimal total = 0m;

        foreach (var itemDto in dto.Items)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == itemDto.ProductId && !p.IsDeleted);

            if (product == null)
                return null;

            if (itemDto.Quantity <= 0)
                return null;

            if (product.StockQuantity < itemDto.Quantity)
                return null;

            var unitPrice = product.Price;
            var lineTotal = unitPrice * itemDto.Quantity;

            product.StockQuantity -= itemDto.Quantity;

            var orderItem = new OrderItem
            {
                ProductId = itemDto.ProductId,
                Quantity = itemDto.Quantity,
                UnitPrice = unitPrice,
                CreatedAt = now,
                UpdatedAt = now,
                IsDeleted = false
            };

            order.OrderItems!.Add(orderItem);
            total += lineTotal;
        }

        order.TotalAmount = total;

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        await _context.Entry(order).Reference(o => o.User).LoadAsync();
        await _context.Entry(order).Collection(o => o.OrderItems!).LoadAsync();
        foreach (var oi in order.OrderItems!)
        {
            await _context.Entry(oi).Reference(x => x.Product).LoadAsync();
        }

        return MapToResponse(order);
    }

    // Siparişi güncelle
    public async Task<OrderResponseDto?> UpdateAsync(int id, OrderUpdateDto dto)
    {
        var order = await _context.Orders
            .Where(o => !o.IsDeleted)
            .Include(o => o.User)
            .Include(o => o.OrderItems)!.ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return null;

        var wasPendingBefore = order.Status == OrderStatus.Pending;

        order.Status = dto.Status;

        if (wasPendingBefore)
        {
            order.PaymentMethod = dto.PaymentMethod;
        }

        order.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return MapToResponse(order);
    }

    // Siparişi sil (soft delete)
    public async Task<bool> DeleteAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);

        if (order == null)
            return false;

        order.IsDeleted = true;
        order.UpdatedAt = DateTime.UtcNow;

        if (order.OrderItems != null)
        {
            foreach (var item in order.OrderItems)
            {
                item.IsDeleted = true;
                item.UpdatedAt = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }

    private static OrderResponseDto MapToResponse(Order order)
    {
        return new OrderResponseDto
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            PaymentMethod = order.PaymentMethod,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            Items = order.OrderItems?
                .Where(oi => !oi.IsDeleted)
                .Select(MapItemToResponse)
                .ToList() ?? new List<OrderItemResponseDto>()
        };
    }

    private static OrderItemResponseDto MapItemToResponse(OrderItem item)
    {
        return new OrderItemResponseDto
        {
            ProductId = item.ProductId,
            ProductName = item.Product?.Name ?? string.Empty,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice
        };
    }
}
