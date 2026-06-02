using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class BlogCategoryConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NameAz)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.SlugAz)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.SlugAz)
            .IsUnique();

        builder.Property(x => x.SlugEn)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.SlugEn)
            .IsUnique();

        builder.Property(x => x.DescriptionAz)
            .HasMaxLength(500);

        builder.Property(x => x.DescriptionEn)
            .HasMaxLength(500);

        builder.Property(x => x.IconUrl)
            .HasMaxLength(500);
    }
}