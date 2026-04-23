using PetCareMini.Application.DTOs.ProductCategory;


namespace PetCareMini.Application.Abstracts.Services;

public interface IProductCategoryService
{
    Task<List<ProductCategoryGetDto>> GetAllAsync();
    Task<ProductCategoryGetDto?> GetByIdAsync(int id);
    Task CreateAsync(ProductCategoryCreateDto dto);
    Task<bool> UpdateAsync(int id, ProductCategoryUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}