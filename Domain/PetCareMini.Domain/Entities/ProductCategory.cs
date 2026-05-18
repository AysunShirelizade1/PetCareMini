using PetCareMini.Domain.Common;

namespace PetCareMini.Domain.Entities;

public class ProductCategory : BaseEntity
{
    public string NameAz { get; set; } = string.Empty;
    public string NameEn { get; set; } = string.Empty;

    public string? DescriptionAz { get; set; }
    public string? DescriptionEn { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
