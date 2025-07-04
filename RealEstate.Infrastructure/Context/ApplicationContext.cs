using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Configurations;

namespace RealEstate.Infrastructure.Context;

public class ApplicationContext: IdentityDbContext<User, IdentityRole<int>, int>
{
    public new virtual DbSet<User> Users { get; set; }
    
    public virtual DbSet<Property> Properties { get; set; }
    
    public virtual DbSet<Agent> Agents { get; set; }
    
    public virtual DbSet<Agency> Agencies { get; set; }
    
    public virtual DbSet<Category> Categories { get; set; }
    
    public virtual DbSet<Review> Reviews { get; set; }

    protected ApplicationContext() { }
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.ApplyConfiguration(new AgencyConfiguration());
        builder.ApplyConfiguration(new AgentConfiguration());
        builder.ApplyConfiguration(new PropertyConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new ReviewConfiguration());
    }
}