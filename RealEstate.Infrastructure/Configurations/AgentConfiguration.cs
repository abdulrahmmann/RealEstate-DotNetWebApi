using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Configurations;

public class AgentConfiguration: BaseEntityConfiguration<Agent>
{
    public override void Configure(EntityTypeBuilder<Agent> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Agents");

        builder.HasIndex(ag => ag.Email);
        
        builder.Property(ag => ag.Name)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(ag => ag.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(ag => ag.Phone)
            .HasMaxLength(20);
        
        builder.Property(ag => ag.ServiceArea)
            .HasMaxLength(200);
        
        builder.OwnsOne(a => a.Address, address =>
        {
            address.Property(a => a.Country).HasMaxLength(50);
            address.Property(a => a.City).HasMaxLength(50);
            address.Property(a => a.Street).HasMaxLength(100);
            address.Property(a => a.ZipCode).HasMaxLength(100);
        });
        
        builder.HasMany(a => a.Properties)
            .WithOne(p => p.Agent)
            .HasForeignKey(p => p.AgentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(a => a.Clients)
            .WithOne(u => u.Agent)
            .HasForeignKey(u => u.AgentId)
            .OnDelete(DeleteBehavior.SetNull); 
        
        builder.HasOne(a => a.Agency)
            .WithMany(age => age.Agents)
            .HasForeignKey(a => a.AgencyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}