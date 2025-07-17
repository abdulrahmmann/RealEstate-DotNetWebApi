using FluentValidation;
using RealEstate.Application.Features.PropertyFeature.DTOs;

namespace RealEstate.Application.Features.PropertyFeature.Validation;

public class AddPropertyValidator: AbstractValidator<AddPropertyDto>
{
    public AddPropertyValidator()
    {
    }
}