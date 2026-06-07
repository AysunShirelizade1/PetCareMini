using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TitleAz)
            .IsRequired()
            .HasMaxLength(300);
        builder.Property(x => x.TitleEn)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.SlugAz)
            .IsRequired()
            .HasMaxLength(300);
        builder.Property(x => x.SlugEn)
            .IsRequired()
            .HasMaxLength(300);

        builder.HasIndex(x => x.SlugAz)
            .IsUnique();
        builder.HasIndex(x => x.SlugEn)
            .IsUnique();

        builder.Property(x => x.ContentAz)
            .IsRequired();

        builder.Property(x => x.ContentEn)
            .IsRequired();

        builder.Property(x => x.SummaryAz)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.SummaryEn)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.CoverImageUrl)
            .HasMaxLength(500);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.RejectionReason)
            .HasMaxLength(500);

        builder.Property(x => x.ViewCount)
            .HasDefaultValue(0);

        builder.Property(x => x.ReadTimeMinutes)
            .HasDefaultValue(0);

        builder.HasOne(x => x.Author)
            .WithMany()
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}