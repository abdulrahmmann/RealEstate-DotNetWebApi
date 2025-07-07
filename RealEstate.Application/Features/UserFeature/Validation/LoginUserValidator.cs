using FluentValidation;
using RealEstate.Application.Features.UserFeature.DTOs;

namespace RealEstate.Application.Features.UserFeature.Validation;

public class LoginUserValidator: AbstractValidator<LoginUserDto>
{
    public LoginUserValidator()
    {
        RuleFor(s => s.Email)
            .EmailAddress().WithMessage("Invalid email format")
            .NotEmpty().WithMessage("Email must not be empty.");
        
        RuleFor(s => s.Password)
            .NotEmpty().WithMessage("Password must not be empty.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Password must contain uppercase, lowercase, number, and special character.");
    }
}