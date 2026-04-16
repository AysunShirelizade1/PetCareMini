using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Gender)
            .HasMaxLength(20);

        builder.Property(x => x.Breed)
            .HasMaxLength(100);

        builder.Property(x => x.Weight)
            .HasColumnType("decimal(5,2)");

        builder.Property(x => x.Notes)
            .HasMaxLength(500);

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasMany(x => x.Appointments)
            .WithOne(x => x.Pet)
            .HasForeignKey(x => x.PetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}