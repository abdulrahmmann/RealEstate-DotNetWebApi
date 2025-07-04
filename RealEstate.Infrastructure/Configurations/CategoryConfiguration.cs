using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Configurations;

public class CategoryConfiguration: BaseEntityConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Categories");
        
        builder.HasIndex(c => c.Name).IsUnique();
        
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(c => c.Description)
            .HasMaxLength(500);
        
        builder.HasMany(c => c.Properties)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}