using FluentValidation;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Validation;

public class AddAgentValidator: AbstractValidator<AddAgentDto>
{
    public AddAgentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email is required.");
        
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required.")
            .Matches(@"^\+?[0-9\s]{7,20}$").WithMessage("Phone must be a valid international number.");

        RuleFor(x => x.ServiceArea)
            .NotEmpty().WithMessage("Service area is required.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("Image URL is required.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Image URL must be a valid URL.");

        RuleFor(x => x.AgencyName)
            .NotEmpty().WithMessage("Agency name is required.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Zip code is required.")
            .MaximumLength(10).WithMessage("Zip code must not exceed 10 characters.");
    }
}