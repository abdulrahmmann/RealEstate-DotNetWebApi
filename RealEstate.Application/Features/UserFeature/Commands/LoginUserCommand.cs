using RealEstate.Application.Features.UserFeature.DTOs;

namespace RealEstate.Application.Features.UserFeature.Commands;

public record LoginUserCommand(LoginUserDto UserDto);