using DKH.Dictionaries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DKH.Dictionaries.Infrastructure.Persistence.Configurations;

public class CityConfiguration : BaseIdConfiguration<CityEntity, string>
{
    public override void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        builder.ToTable("Cities");

        builder.Property(entity => entity.Name).IsRequired()
            .HasMaxLength(100);

        base.Configure(builder);
    }
}