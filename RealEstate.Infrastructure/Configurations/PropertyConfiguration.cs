using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Configurations;

public class PropertyConfiguration: BaseEntityConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Properties");
        
        builder.HasIndex(p => p.Type);
        builder.HasIndex(p => p.Status);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(p => p.Description)
            .HasMaxLength(1000);
        
        builder.Property(p => p.Type)
            .IsRequired()
            .HasConversion<string>(); 
        
        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>();
        
        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)"); 
        
        builder.OwnsOne(p => p.Address, address =>
        {
            address.Property(a => a.Country).HasMaxLength(50);
            address.Property(a => a.City).HasMaxLength(50);
            address.Property(a => a.Street).HasMaxLength(100);
            address.Property(a => a.ZipCode).HasMaxLength(100);
        });
        
        builder.OwnsOne(p => p.Amenities);
        builder.OwnsOne(p => p.Facilities);
        
        builder.HasMany(p => p.Reviews)
            .WithOne(r => r.Property)
            .HasForeignKey(r => r.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(p => p.Category)
            .WithMany(c => c.Properties)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}