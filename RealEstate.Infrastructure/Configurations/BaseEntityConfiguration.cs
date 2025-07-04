using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Common;

namespace RealEstate.Infrastructure.Configurations;

public class BaseEntityConfiguration<T>: IEntityTypeConfiguration<T> where T : BaseEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(be => be.Id);

        builder.Property(be => be.Id).UseIdentityColumn();
        
        builder.Property(e => e.CreatedOn)
            .HasDefaultValueSql("GETUTCDATE()");
        
        builder.Property(e => e.ModifiedOn)
            .IsRequired(false);
    }
}