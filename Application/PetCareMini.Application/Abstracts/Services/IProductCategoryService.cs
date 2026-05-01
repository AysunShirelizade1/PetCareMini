using PetCareMini.Application.DTOs.ProductCategory;

namespace PetCareMini.Application.Abstracts.Services;

public interface IProductCategoryService
{
    Task<List<ProductCategoryGetDto>> GetAllAsync(string lang);

    Task<ProductCategoryGetDto?> GetByIdAsync(int id, string lang);

    Task CreateAsync(ProductCategoryCreateDto dto);

    Task<bool> UpdateAsync(int id, ProductCategoryUpdateDto dto);

    Task<bool> DeleteAsync(int id);
}