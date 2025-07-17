namespace RealEstate.Application.Features.PropertyFeature.DTOs.ValueObjectDTO;

public record AddressDto(
    string Country,
    string City,
    string? Street,
    string? ZipCode
);