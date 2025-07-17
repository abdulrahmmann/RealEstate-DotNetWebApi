namespace RealEstate.Application.Features.PropertyFeature.DTOs;

public record AddressDto(
    string Country,
    string City,
    string? Street,
    string? ZipCode
);