using DKH.Dictionaries.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DKH.Dictionaries.Infrastructure.Persistence.Configurations;

public class StateTranslationConfiguration : BaseIdConfiguration<StateTranslationEntity, string>
{
    public override void Configure(EntityTypeBuilder<StateTranslationEntity> builder)
    {
        builder.ToTable("StateTranslations");

        builder.Property(entity => entity.Name).HasMaxLength(100);

        builder.HasOne(x => x.State).WithMany(x => x.Translations)
            .HasForeignKey(x => x.StateId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Language).WithMany()
            .HasForeignKey(x => x.LanguageId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        base.Configure(builder);
    }
}