using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.Domain.Entities;

namespace DKH.Dictionaries.Infrastructure.Persistence.Configurations;

public abstract class BaseIdConfiguration<TEntity, TPrimaryKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TPrimaryKey>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
    }
}