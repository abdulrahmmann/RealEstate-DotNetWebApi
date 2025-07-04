using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Common;

namespace RealEstate.Infrastructure.Configurations;

public abstract class BaseEntityConfiguration<T>: IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(be => be.Id);

        builder.Property(be => be.Id).UseIdentityColumn();
        
        builder.Property(e => e.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");
        
        builder.Property(e => e.ModifiedOn)
            .IsRequired(false);
    }
}