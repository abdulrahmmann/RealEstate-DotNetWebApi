using FluentValidation;
using RealEstate.Application.Features.UserFeature.DTOs;

namespace RealEstate.Application.Features.UserFeature.Validation;

public class RegisterUserValidator: AbstractValidator<RegisterUserDto>
{
    public RegisterUserValidator()
    {
        RuleFor(s => s.Email)
            .EmailAddress().WithMessage("Invalid email format")
            .NotEmpty().WithMessage("Email must not be empty.");

        RuleFor(s => s.Password)
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
            .NotEmpty().WithMessage("Password must not be empty.")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Password must contain uppercase, lowercase, number, and special character.");
        
        RuleFor(s => s.Gender)
            .NotEmpty().WithMessage("Gender is required.")
            .Must(g => g.Equals("Male", StringComparison.OrdinalIgnoreCase) || g.Equals("Female", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Gender must be either 'Male' or 'Female'.");
    }
}