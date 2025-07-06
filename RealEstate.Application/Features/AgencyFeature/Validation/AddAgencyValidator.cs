using FluentValidation;
using RealEstate.Application.Features.AgencyFeature.DTOs;

namespace RealEstate.Application.Features.AgencyFeature.Validation;

public abstract class AddAgencyValidator: AbstractValidator<AgencyDto>
{
    protected AddAgencyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Agency name is required.")
            .MaximumLength(100).WithMessage("Agency name must not exceed 100 characters.");

        RuleFor(x => x.LicenseNumber)
            .NotEmpty().WithMessage("License number is required.")
            .MaximumLength(50).WithMessage("License number must not exceed 50 characters.");

        RuleFor(x => x.TaxNumber)
            .NotEmpty().WithMessage("Tax number is required.")
            .MaximumLength(50).WithMessage("Tax number must not exceed 50 characters.");
    }
}