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

    public async Task<List<ProductCategoryGetDto>> GetAllAsync(string lang)
    {
        var data = await _repository.GetAllAsync();

        return data.Select(x => MapToDto(x, lang)).ToList();
    }

    public async Task<ProductCategoryGetDto?> GetByIdAsync(int id, string lang)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return null;

        return MapToDto(entity, lang);
    }

    public async Task CreateAsync(ProductCategoryCreateDto dto)
    {
        var entity = new ProductCategory
        {
            NameAz = dto.NameAz,
            NameEn = dto.NameEn,
            DescriptionAz = dto.DescriptionAz,
            DescriptionEn = dto.DescriptionEn
        };

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, ProductCategoryUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return false;

        entity.NameAz = dto.NameAz;
        entity.NameEn = dto.NameEn;
        entity.DescriptionAz = dto.DescriptionAz;
        entity.DescriptionEn = dto.DescriptionEn;

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

    private ProductCategoryGetDto MapToDto(ProductCategory category, string lang)
    {
        var isEn = lang.ToLower() == "en";

        return new ProductCategoryGetDto
        {
            Id = category.Id,
            Name = isEn ? category.NameEn : category.NameAz,
            Description = isEn ? category.DescriptionEn : category.DescriptionAz
        };
    }
}