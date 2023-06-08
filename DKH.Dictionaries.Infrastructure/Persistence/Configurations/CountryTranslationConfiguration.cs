using DKH.Dictionaries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DKH.Dictionaries.Infrastructure.Persistence.Configurations;

public class CountryTranslationConfiguration : BaseIdConfiguration<CountryTranslationEntity, string>
{
    public override void Configure(EntityTypeBuilder<CountryTranslationEntity> builder)
    {
        builder.ToTable("CountryTranslations");

        builder.Property(entity => entity.Name).HasMaxLength(100);

        builder.HasOne(x => x.Country).WithMany(x => x.Translations)
            .HasForeignKey(x => x.CountryId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        
        base.Configure(builder);
    }
}