using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Configurations;

public class ReviewConfiguration: BaseEntityConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Reviews");
        
        builder.HasIndex(r => r.Rating);
        
        builder.Property(r => r.Rating)
            .IsRequired();
        
        builder.Property(r => r.Comment)
            .HasMaxLength(500);
        
        builder.HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction); 
        
        builder.HasOne(r => r.Property)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}