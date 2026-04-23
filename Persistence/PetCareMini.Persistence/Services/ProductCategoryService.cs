using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.ProductCategory;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _repository;

    public ProductCategoryService(IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductCategoryGetDto>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();

        return data.Select(x => new ProductCategoryGetDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }

    public async Task<ProductCategoryGetDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return null;

        return new ProductCategoryGetDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public async Task CreateAsync(ProductCategoryCreateDto dto)
    {
        var entity = new ProductCategory
        {
            Name = dto.Name
        };

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, ProductCategoryUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return false;

        entity.Name = dto.Name;

        _repository.Update(entity);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return false;

        _repository.Delete(entity);
        await _repository.SaveChangesAsync();

        return true;
    }
}