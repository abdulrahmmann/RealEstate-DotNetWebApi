using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Repository;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Infrastructure;

public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        // REGISTER GENERIC REPOSITORY  
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        // REGISTER UNIT OF WORK    
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // REGISTER REPOSITORIES    
        services.AddTransient<IPropertyRepository, PropertyRepository>();
        
        return services;
    }
}