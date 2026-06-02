using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class BlogTagConfiguration : IEntityTypeConfiguration<BlogTag>
{
    public void Configure(EntityTypeBuilder<BlogTag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.NameAz)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.NameEn)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.SlugAz)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(x => x.SlugEn)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.SlugAz)
            .IsUnique();
        builder.HasIndex(x => x.SlugEn)
            .IsUnique();
    }
}