using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Abstracts.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task AddAsync(Product entity);
    void Update(Product entity);
    void Delete(Product entity);
    Task SaveChangesAsync();
}