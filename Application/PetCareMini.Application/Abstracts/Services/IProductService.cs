using PetCareMini.Application.DTOs.Product;

namespace PetCareMini.Application.Abstracts.Services;

public interface IProductService
{
    Task<List<ProductGetDto>> GetAllAsync();
    Task<ProductGetDto?> GetByIdAsync(int id);
    Task CreateAsync(ProductCreateDto dto);
    Task<bool> UpdateAsync(int id, ProductUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}