using PetCareMini.Application.DTOs.Product;

namespace PetCareMini.Application.Abstracts.Services;

public interface IProductService
{
    Task<List<ProductGetDto>> GetAllAsync(string lang);

    Task<ProductGetDto?> GetByIdAsync(int id, string lang);

    Task CreateAsync(ProductCreateDto dto);

    Task<bool> UpdateAsync(int id, ProductUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}