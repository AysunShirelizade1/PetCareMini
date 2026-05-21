using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class BlogAuthorProfileConfiguration : IEntityTypeConfiguration<BlogAuthorProfile>
{
    public void Configure(EntityTypeBuilder<BlogAuthorProfile> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Bio)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.ProfileImageUrl)
            .HasMaxLength(500);

        builder.Property(x => x.WebsiteUrl)
            .HasMaxLength(200);

        builder.Property(x => x.InstagramUrl)
            .HasMaxLength(200);

        builder.Property(x => x.LinkedInUrl)
            .HasMaxLength(200);

        builder.Property(x => x.TotalPosts)
            .HasDefaultValue(0);

        // Hər userin yalnız 1 profili var
        builder.HasIndex(x => x.UserId)
            .IsUnique();

        builder.HasOne(x => x.User)
            .WithOne(x => x.BlogAuthorProfile)
            .HasForeignKey<BlogAuthorProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}