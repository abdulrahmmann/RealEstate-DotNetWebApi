namespace RealEstate.Application.Features.PropertyFeature.DTOs.ValueObjectDTO;

public record FacilitiesDto(
    int Area,
    int Rooms,
    int Kitchens,
    int Balconies,
    int Baths,
    int Beds
);