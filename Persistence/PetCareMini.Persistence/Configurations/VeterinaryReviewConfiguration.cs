using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetCareMini.Persistence.Configurations;

public class VeterinaryReviewConfiguration
    : IEntityTypeConfiguration<VeterinaryReview>
{
    public void Configure(EntityTypeBuilder<VeterinaryReview> builder)
    {
        builder.HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Veterinarian)
            .WithMany()
            .HasForeignKey(r => r.VeterinarianId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(r => r.Service)
            .WithMany()
            .HasForeignKey(r => r.ServiceId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(r => r.Appointment)
            .WithMany()
            .HasForeignKey(r => r.AppointmentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Property(r => r.Comment).HasMaxLength(500);
        builder.Property(r => r.Rating).IsRequired();
    }
}