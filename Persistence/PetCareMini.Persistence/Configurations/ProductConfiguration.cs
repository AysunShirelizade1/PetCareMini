using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}