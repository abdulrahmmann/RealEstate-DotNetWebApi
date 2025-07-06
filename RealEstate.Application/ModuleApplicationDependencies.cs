using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Features.AgencyFeature.Validation;

namespace RealEstate.Application;

public static class ModuleApplicationDependencies
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        // Register Mediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        // Register FLUENT VALIDATION
        services.AddValidatorsFromAssemblyContaining<AddAgencyValidator>();

        
        return services;
    }
}