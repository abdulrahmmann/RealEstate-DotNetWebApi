using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.IRepository;
using RealEstate.Infrastructure.Repository;

namespace RealEstate.Infrastructure;

public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        // REGISTER GENERIC REPOSITORY  
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        
        return services;
    }
}