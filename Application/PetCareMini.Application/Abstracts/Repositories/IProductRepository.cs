using PetCareMini.Domain.Entities;
namespace PetCareMini.Application.Interfaces.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    void Update(Product product);
    void Delete(Product product);
    Task AddAsync(Product product);
    Task SaveChangesAsync();
}