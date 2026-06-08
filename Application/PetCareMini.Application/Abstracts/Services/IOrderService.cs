
namespace PetCareMini.Application.Abstracts.Services;

using PetCareMini.Application.DTOs.Order;
using PetCareMini.Domain.Enums;


public interface IOrderService
{
    Task<List<OrderAdminDto>> GetAllAsync(string lang, string? status, int page, int pageSize);

    Task<OrderGetDto> CheckoutAsync(int userId, string lang, string? couponCode);

    Task<List<OrderGetDto>> GetMyOrdersAsync(int userId, string lang);
    Task CancelOrderAsync(int userId, int orderId);

    Task UpdateStatusAsync(int orderId, OrderStatus status);
    Task RejectOrderAsync(int orderId, string reason);
}
