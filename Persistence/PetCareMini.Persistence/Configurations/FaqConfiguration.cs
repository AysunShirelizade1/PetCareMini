using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetCareMini.Domain.Entities;

namespace PetCareMini.Persistence.Configurations;

public class FaqConfiguration : IEntityTypeConfiguration<Faq>
{
    public void Configure(EntityTypeBuilder<Faq> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.QuestionAz)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.QuestionEn)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.AnswerAz)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.AnswerEn)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);
    }
}