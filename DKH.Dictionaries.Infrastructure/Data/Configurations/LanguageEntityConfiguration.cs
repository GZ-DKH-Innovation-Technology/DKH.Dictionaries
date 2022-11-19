using DKH.Dictionaries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DKH.Dictionaries.Infrastructure.Data.Configurations;

public class LanguageEntityConfiguration : BaseIdConfiguration<LanguageEntity, int>
{
    public override void Configure(EntityTypeBuilder<LanguageEntity> builder)
    {
        builder.ToTable("Languages");

        builder.Property(entity => entity.CultureName).HasMaxLength(50);
        builder.Property(entity => entity.DisplayName).HasMaxLength(50);
        builder.Property(entity => entity.EnglishName).HasMaxLength(50);
        builder.Property(entity => entity.NativeName).HasMaxLength(50);
        builder.Property(entity => entity.TwoLetterLanguageName).HasMaxLength(2);
        builder.Property(entity => entity.ThreeLetterLanguageName).HasMaxLength(3);

        base.Configure(builder);
    }
}