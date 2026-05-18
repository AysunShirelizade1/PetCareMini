using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Abstracts.Repositories;

public interface IProductCategoryRepository
{
    Task<List<ProductCategory>> GetAllAsync();
    Task<ProductCategory?> GetByIdAsync(int id);
    void Update(ProductCategory productCategory);
    void Delete(ProductCategory productCategory);
    Task AddAsync(ProductCategory productCategory);
    Task SaveChangesAsync();
}