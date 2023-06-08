using DKH.Dictionaries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DKH.Dictionaries.Infrastructure.Persistence.Configurations;

public class CityTranslationConfiguration : BaseIdConfiguration<CityTranslationEntity, string>
{
    public override void Configure(EntityTypeBuilder<CityTranslationEntity> builder)
    {
        builder.ToTable("CityTranslations");

        builder.Property(entity => entity.Name).IsRequired().HasMaxLength(100);

        builder.HasOne(x => x.City).WithMany(x => x.Translations)
            .HasForeignKey(x => x.CityId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Language).WithMany()
            .HasForeignKey(x => x.LanguageId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        base.Configure(builder);
    }
}