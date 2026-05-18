using Microsoft.EntityFrameworkCore;
using PetCareMini.Application.Abstracts.Repositories;
using PetCareMini.Application.Abstracts.Services;
using PetCareMini.Application.Common;
using PetCareMini.Application.DTOs.Product;
using PetCareMini.Domain.Entities;
using PetCareMini.Persistence.Contexts;

namespace PetCareMini.Persistence.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IProductRepository _productRepo;

    public ProductService(AppDbContext context, IProductRepository productRepo)
    {
        _context = context;
        _productRepo = productRepo;
    }

    public async Task<PagedResult<ProductGetDto>> GetAllAsync(ProductQueryDto query)
    {
        var query_ = _context.Products
            .Include(p => p.Category)
            .Where(p => p.IsActive)
            .AsQueryable();

        if (query.CategoryId.HasValue)
            query_ = query_.Where(p => p.CategoryId == query.CategoryId);

        if (query.MinPrice.HasValue)
            query_ = query_.Where(p => p.Price >= query.MinPrice);

        if (query.MaxPrice.HasValue)
            query_ = query_.Where(p => p.Price <= query.MaxPrice);

        if (!string.IsNullOrWhiteSpace(query.Search))
            query_ = query_.Where(p =>
                p.NameAz.Contains(query.Search) ||
                p.NameEn.Contains(query.Search));

        query_ = query.SortBy switch
        {
            "priceAsc" => query_.OrderBy(p => p.Price),
            "priceDesc" => query_.OrderByDescending(p => p.Price),
            "nameAsc" => query_.OrderBy(p => query.Lang == "en" ? p.NameEn : p.NameAz),
            "nameDesc" => query_.OrderByDescending(p => query.Lang == "en" ? p.NameEn : p.NameAz),
            _ => query_.OrderByDescending(p => p.Id)
        };

        var totalCount = await query_.CountAsync();

        var items = await query_
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(p => new ProductGetDto
            {
                Id = p.Id,
                Name = query.Lang == "en" ? p.NameEn : p.NameAz,
                Description = query.Lang == "en" ? p.DescriptionEn : p.DescriptionAz,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                CategoryName = query.Lang == "en" ? p.Category.NameEn : p.Category.NameAz
            })
            .ToListAsync();

        return new PagedResult<ProductGetDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = query.Page,
            PageSize = query.PageSize
        };
    }

    public async Task<ProductGetDto?> GetByIdAsync(int id, string lang)
    {
        var p = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(x => x.Id == id && x.IsActive);

        if (p is null)
            throw new KeyNotFoundException($"Product with id {id} not found.");

        return new ProductGetDto
        {
            Id = p.Id,
            Name = lang == "en" ? p.NameEn : p.NameAz,
            Description = lang == "en" ? p.DescriptionEn : p.DescriptionAz,
            Price = p.Price,
            DiscountPrice = p.DiscountPrice,
            StockQuantity = p.StockQuantity,
            ImageUrl = p.ImageUrl,
            CategoryName = lang == "en" ? p.Category.NameEn : p.Category.NameAz
        };
    }

    public async Task<List<ProductGetDto>> GetRecommendedAsync(
        int productId, string lang = "az", int count = 6)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == productId && p.IsActive);

        if (product is null)
            throw new KeyNotFoundException($"Product with id {productId} not found.");

        return await _context.Products
            .Include(p => p.Category)
            .Where(p =>
               p.IsActive &&
               p.CategoryId == product.CategoryId &&
               p.Id != productId)
            .OrderBy(p => Guid.NewGuid())
            .Take(count)
            .Select(p => new ProductGetDto
            {
                Id = p.Id,
                Name = lang == "en" ? p.NameEn : p.NameAz,
                Description = lang == "en" ? p.DescriptionEn : p.DescriptionAz,
                Price = p.Price,
                DiscountPrice = p.DiscountPrice,
                StockQuantity = p.StockQuantity,
                ImageUrl = p.ImageUrl,
                CategoryName = lang == "en" ? p.Category.NameEn : p.Category.NameAz
            })
            .ToListAsync();
    }

    public async Task CreateAsync(ProductCreateDto dto)
    {
        await _context.Products.AddAsync(new Product
        {
            NameAz = dto.NameAz,
            NameEn = dto.NameEn,
            DescriptionAz = dto.DescriptionAz,
            DescriptionEn = dto.DescriptionEn,
            Price = dto.Price,
            DiscountPrice = dto.DiscountPrice,
            StockQuantity = dto.StockQuantity,
            ImageUrl = dto.ImageUrl,
            CategoryId = dto.CategoryId,
            IsActive = true
        });
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        product.NameAz = dto.NameAz;
        product.NameEn = dto.NameEn;
        product.DescriptionAz = dto.DescriptionAz;
        product.DescriptionEn = dto.DescriptionEn;
        product.Price = dto.Price;
        product.DiscountPrice = dto.DiscountPrice; 
        product.StockQuantity = dto.StockQuantity;
        product.ImageUrl = dto.ImageUrl;
        product.IsActive = dto.IsActive;
        product.CategoryId = dto.CategoryId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null) return false;

        product.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }
}