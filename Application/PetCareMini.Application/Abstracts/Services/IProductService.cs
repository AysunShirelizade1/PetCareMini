using PetCareMini.Application.Common;
using PetCareMini.Application.DTOs.Product;

namespace PetCareMini.Application.Abstracts.Services;

public interface IProductService
{
    Task<PagedResult<ProductGetDto>> GetAllAsync(ProductQueryDto query);
    Task<ProductGetDto?> GetByIdAsync(int id, string lang = "az");
    Task<List<ProductGetDto>> GetRecommendedAsync(int productId, string langn = "az", int count = 6);
    Task CreateAsync(ProductCreateDto dto);
    Task<bool> UpdateAsync(int id, ProductUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}