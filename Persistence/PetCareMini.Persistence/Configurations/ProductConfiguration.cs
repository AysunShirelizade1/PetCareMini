using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NameAz)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.DescriptionAz)
            .HasMaxLength(1000);

        builder.Property(x => x.DescriptionEn)
            .HasMaxLength(1000);

        builder.Property(x => x.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(255);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);
    }
}