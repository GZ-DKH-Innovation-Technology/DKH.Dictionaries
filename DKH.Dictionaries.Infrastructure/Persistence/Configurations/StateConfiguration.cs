using DKH.Dictionaries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DKH.Dictionaries.Infrastructure.Persistence.Configurations;

public class StateConfiguration : BaseIdConfiguration<StateEntity, string>
{
    public override void Configure(EntityTypeBuilder<StateEntity> builder)
    {
        builder.ToTable("States");

        builder.Property(entity => entity.Name)
            .HasMaxLength(100);

        builder.Property(entity => entity.Code)
            .HasMaxLength(5);

        builder.Property(entity => entity.Type)
            .HasMaxLength(100);

        base.Configure(builder);
    }
}