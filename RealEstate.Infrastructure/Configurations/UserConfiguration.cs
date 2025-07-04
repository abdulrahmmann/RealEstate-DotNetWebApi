using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id).UseIdentityColumn();
        
        builder.HasIndex(u => u.UserName).IsUnique();
        
        builder.OwnsOne(a => a.Address, address =>
        {
            address.Property(a => a.Country).HasMaxLength(50);
            address.Property(a => a.City).HasMaxLength(50);
            address.Property(a => a.Street).HasMaxLength(100);
            address.Property(a => a.ZipCode).HasMaxLength(100);
        });
    }
}