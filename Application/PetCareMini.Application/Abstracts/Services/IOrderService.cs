using PetCareMini.Application.DTOs.Order;

namespace PetCareMini.Application.Abstracts.Services;

public interface IOrderService
{
    Task<OrderGetDto?> CheckoutAsync(int userId, string lang);

    Task<List<OrderGetDto>> GetMyOrdersAsync(int userId, string lang);
}