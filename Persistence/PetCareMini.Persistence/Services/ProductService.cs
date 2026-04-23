using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.DTOs.Product;
using PetCareMini.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Persistence.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductCategoryRepository _categoryRepository;

    public ProductService(
    IProductRepository repository,
    IProductCategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task<List<ProductGetDto>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();

        return data.Select(x => new ProductGetDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            StockQuantity = x.StockQuantity,
            ImageUrl = x.ImageUrl,
            CategoryName = x.Category.Name
        }).ToList();
    }

    public async Task<ProductGetDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return null;

        return new ProductGetDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            StockQuantity = entity.StockQuantity,
            ImageUrl = entity.ImageUrl,
            CategoryName = entity.Category.Name
        };
    }

    public async Task CreateAsync(ProductCreateDto dto)
    {
        var categoryExists = await _categoryRepository.GetByIdAsync(dto.CategoryId);

        if (categoryExists is null)
            throw new Exception("Category not found");

        var entity = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            StockQuantity = dto.StockQuantity,
            ImageUrl = dto.ImageUrl,
            CategoryId = dto.CategoryId
        };

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity is null)
            return false;

        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.Price = dto.Price;
        entity.StockQuantity = dto.StockQuantity;
        entity.ImageUrl = dto.ImageUrl;
        entity.CategoryId = dto.CategoryId;

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
