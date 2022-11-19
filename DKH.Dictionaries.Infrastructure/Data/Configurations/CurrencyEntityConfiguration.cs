using DKH.Dictionaries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DKH.Dictionaries.Infrastructure.Data.Configurations;

public class CurrencyEntityConfiguration : BaseIdConfiguration<CurrencyEntity, int>
{
    public override void Configure(EntityTypeBuilder<CurrencyEntity> builder)
    {
        builder.ToTable("Currencies");

        builder.Property(entity => entity.Name).HasMaxLength(100);
        builder.Property(entity => entity.Code).HasMaxLength(3);
        builder.Property(entity => entity.Symbol).HasMaxLength(10);

        base.Configure(builder);
    }
}