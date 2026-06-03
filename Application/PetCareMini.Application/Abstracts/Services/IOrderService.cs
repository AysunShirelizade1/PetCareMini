using PetCareMini.Application.DTOs.Order;

namespace PetCareMini.Application.Abstracts.Services;

public interface IOrderService
{
    Task<OrderGetDto?> CheckoutAsync(int userId, string lang, string? couponCode = null);

    Task<List<OrderGetDto>> GetMyOrdersAsync(int userId, string lang);
    Task<List<OrderAdminDto>> GetAllAsync(string lang, string? status = null, int page = 1, int pageSize = 20);
}