using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RealEstate.Application.Common;
using RealEstate.Application.Features.UserFeature.DTOs;
using RealEstate.Application.helper;
using RealEstate.Domain.Entities;
using RealEstate.Domain.ValueObjects;

namespace RealEstate.Application.Features.UserFeature.Commands;

public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, AuthResponse>
{
    #region Instance Fields
    private readonly UserManager<User>  _userManager;
    private readonly IConfiguration  _configuration;
    private readonly IValidator<RegisterUserDto> _validator;
    #endregion
    
    #region Constructor
    public RegisterUserCommandHandler(UserManager<User> userManager, IConfiguration configuration, IValidator<RegisterUserDto> validator)
    {
        _userManager = userManager;
        _configuration = configuration;
        _validator = validator;
    }
    #endregion
    
    public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = request.UserDto;

            var validationResult = await _validator.ValidateAsync(user, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new AuthResponse(string.Empty, false, $"Validation Error: {validationErrors}", HttpStatusCode.UnprocessableEntity);
            }
            
            var isEmailExist = await _userManager.FindByEmailAsync(user.Email);
            var isUsernameExist = await _userManager.FindByNameAsync(user.UserName);
            
            if (isEmailExist != null)
            {
                return new AuthResponse(string.Empty, false, "Email Is Already Exist!", HttpStatusCode.Conflict);
            }

            if (isUsernameExist != null)
            {
                return new AuthResponse(string.Empty, false, "UserName Is Already Exist!", HttpStatusCode.Conflict);
            }

            var newUser = new User
            {
                Email = user.Email,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.DateOfBirth,
                Gender = user.Gender,
                Address = new Address(user.Country, user.City),
            };

            var isCreated = await _userManager.CreateAsync(newUser, user.Password);

            if (!isCreated.Succeeded)
            {
                return new AuthResponse(string.Empty, false, "User Registration Failed!", HttpStatusCode.InternalServerError);
            }
            
            var generatedToken = JwtTokenHelper.GenerateJwtToken(newUser, _configuration);
                
            return new AuthResponse(generatedToken, true, "User Registration Success!", HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            return new AuthResponse(string.Empty, false, e.Message, HttpStatusCode.InternalServerError);
        }
    }
}