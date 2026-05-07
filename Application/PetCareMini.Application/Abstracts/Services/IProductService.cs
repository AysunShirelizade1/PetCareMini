using PetCareMini.Application.Common;
using PetCareMini.Application.DTOs.Product;

namespace PetCareMini.Application.Abstracts.Services;

public interface IProductService
{
    Task<PagedResult<ProductGetDto>> GetAllAsync(ProductQueryDto query);
    Task<ProductGetDto?> GetByIdAsync(int id, string lang = "az");
    Task CreateAsync(ProductCreateDto dto);
    Task<bool> UpdateAsync(int id, ProductUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}