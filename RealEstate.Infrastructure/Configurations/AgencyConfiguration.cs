using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Configurations;

public class AgencyConfiguration: BaseEntityConfiguration<Agency>
{
    public void Configure(EntityTypeBuilder<Agency> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Agencies");
        
        builder.HasIndex(a => a.Name).IsUnique();
        
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(a => a.LicenseNumber)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(a => a.TaxNumber)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.HasMany(a => a.Agents)
            .WithOne(ag => ag.Agency)
            .HasForeignKey(ag => ag.AgencyId)
            .OnDelete(DeleteBehavior.Restrict); 
    }
}