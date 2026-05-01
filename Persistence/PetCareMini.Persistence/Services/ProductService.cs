using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Product;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductGetDto>> GetAllAsync(string lang)
    {
        var products = await _repository.GetAllAsync();

        return products.Select(x => MapToDto(x, lang)).ToList();
    }

    public async Task<ProductGetDto?> GetByIdAsync(int id, string lang)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            return null;

        return MapToDto(product, lang);
    }

    public async Task CreateAsync(ProductCreateDto dto)
    {
        var product = new Product
        {
            NameAz = dto.NameAz,
            NameEn = dto.NameEn,
            DescriptionAz = dto.DescriptionAz,
            DescriptionEn = dto.DescriptionEn,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            ImageUrl = dto.ImageUrl,
            CategoryId = dto.CategoryId,
            IsActive = true
        };

        await _repository.AddAsync(product);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            return false;

        product.NameAz = dto.NameAz;
        product.NameEn = dto.NameEn;
        product.DescriptionAz = dto.DescriptionAz;
        product.DescriptionEn = dto.DescriptionEn;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.ImageUrl = dto.ImageUrl;
        product.CategoryId = dto.CategoryId;
        product.IsActive = dto.IsActive;

        _repository.Update(product);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            return false;

        _repository.Delete(product);
        await _repository.SaveChangesAsync();

        return true;
    }

    private ProductGetDto MapToDto(Product product, string lang)
    {
        var isEn = lang.ToLower() == "en";

        return new ProductGetDto
        {
            Id = product.Id,
            Name = isEn ? product.NameEn : product.NameAz,
            Description = isEn ? product.DescriptionEn : product.DescriptionAz,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            ImageUrl = product.ImageUrl,
            CategoryName = isEn ? product.Category.NameEn : product.Category.NameAz
        };
    }
}