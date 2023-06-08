using DKH.Dictionaries.Domain.Entities;
using DKH.Dictionaries.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DKH.Dictionaries.Infrastructure.Persistence.Configurations;

public class CountryConfiguration : BaseIdConfiguration<CountryEntity, string>
{
    public override void Configure(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.ToTable("Countries");

        builder.Property(entity => entity.Name)
            .HasMaxLength(100);

        builder.Property(entity => entity.NativeName)
            .HasMaxLength(50);

        builder.Property(entity => entity.TwoLetterCode)
            .HasMaxLength(2)
            .HasConversion(new EnumToStringConverter<CountryTwoLetterCodeEnum>());

        builder.Property(entity => entity.ThreeLetterCode)
            .HasMaxLength(3)
            .HasConversion(new EnumToStringConverter<CountryThreeLetterCodeEnum>());

        builder.Property(entity => entity.NumericCode);
        
        base.Configure(builder);
    }
}