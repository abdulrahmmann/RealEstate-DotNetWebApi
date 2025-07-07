namespace RealEstate.Application.Features.UserFeature.DTOs;

public record RegisterUserDto(string Email, string UserName, string Password, DateOnly DateOfBirth, string PhoneNumber, string Gender, string Country, string City, string Street, string ZipCode);