using DKH.Dictionaries.Domain.Entities;
using DKH.Dictionaries.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DKH.Dictionaries.Infrastructure.Data.Configurations;

public class CountryConfiguration : BaseIdConfiguration<CountryEntity, int>
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

        builder.Property(entity => entity.PhoneCode).HasMaxLength(20);

        builder.Property(entity => entity.Capital).HasMaxLength(20);

        builder.Property(entity => entity.Tld).HasMaxLength(10);

        builder.Property(entity => entity.Region).HasMaxLength(20);

        builder.Property(entity => entity.Subregion).HasMaxLength(50);

        base.Configure(builder);
    }
}