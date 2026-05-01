using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
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
            .HasColumnType("decimal(10,2)");

        builder.Property(x => x.ImageUrl)
            .HasMaxLength(255);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Appointments)
            .WithOne(x => x.Service)
            .HasForeignKey(x => x.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}