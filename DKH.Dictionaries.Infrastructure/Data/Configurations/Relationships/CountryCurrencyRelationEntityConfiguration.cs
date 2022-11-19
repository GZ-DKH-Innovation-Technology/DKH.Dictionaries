using DKH.Dictionaries.Domain.Entities.Relationships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DKH.Dictionaries.Infrastructure.Data.Configurations.Relationships;

public class CountryCurrencyRelationEntityConfiguration : BaseIdConfiguration<CountryCurrencyRelationEntity, int>
{
    public override void Configure(EntityTypeBuilder<CountryCurrencyRelationEntity> builder)
    {
        builder.ToTable("CountryCurrencyRelations");

        builder.HasKey(entity => new { entity.CountryId, entity.CurrencyId });

        builder.HasOne(entity => entity.Country)
            .WithMany(entity => entity.Currencies)
            .HasForeignKey(entity => entity.CountryId);

        builder.HasOne(entity => entity.Currency)
            .WithMany(entity => entity.Countries)
            .HasForeignKey(entity => entity.CountryId);

        base.Configure(builder);
    }
}