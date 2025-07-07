using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RealEstate.Application.Common;
using RealEstate.Application.Features.UserFeature.DTOs;
using RealEstate.Application.helper;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.UserFeature.Commands;

public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand, AuthResponse>
{
    #region Instance Fields
    private readonly UserManager<User>  _userManager;
    private readonly IConfiguration  _configuration;
    private readonly IValidator<LoginUserDto> _validator;
    #endregion
    
    #region Constructor
    public LoginUserCommandHandler(UserManager<User> userManager, IConfiguration configuration, IValidator<LoginUserDto> validator)
    {
        _userManager = userManager;
        _configuration = configuration;
        _validator = validator;
    }
    #endregion

    public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
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

            if (isEmailExist == null)
            {
                return new AuthResponse(string.Empty, false, "User does not exist!", HttpStatusCode.NotFound);
            }

            
            var generatedToken = JwtTokenHelper.GenerateJwtToken(isEmailExist, _configuration);
            
            return new AuthResponse(generatedToken, true, "User Login Success!", HttpStatusCode.OK);
        }
        catch (Exception e)
        {
            return new AuthResponse(string.Empty, false, e.Message, HttpStatusCode.InternalServerError);
        }
    }
}