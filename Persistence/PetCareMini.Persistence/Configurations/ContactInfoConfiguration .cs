using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
{
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        builder.HasData(new ContactInfo
        {
            Id = 1,
            PhoneNumber = "+00 12345678",
            Email = "hi@petfun.com",
            Address = "Germany — 785 15h Street, Office 478, Berlin",
            WorkingHours = "Monday - Friday: 9:30 AM - 5:00 PM"
        });
    }
}