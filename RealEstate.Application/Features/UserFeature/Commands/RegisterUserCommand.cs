using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.UserFeature.DTOs;

namespace RealEstate.Application.Features.UserFeature.Commands;

public record RegisterUserCommand(RegisterUserDto UserDto): IRequest<AuthResponse>;