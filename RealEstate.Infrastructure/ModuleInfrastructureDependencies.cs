using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Repository;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Infrastructure;

public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        // REGISTER UNIT OF WORK    
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // REGISTER GENERIC REPOSITORY  
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        // REGISTER REPOSITORIES    
        services.AddScoped<IPropertyRepository, PropertyRepository>();
        services.AddScoped<IAgentRepository, AgentRepository>();
        services.AddScoped<IAgencyRepository, AgencyRepository>();
        
        return services;
    }
}