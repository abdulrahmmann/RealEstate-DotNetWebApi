namespace RealEstate.Application.Features.PropertyFeature.DTOs.ValueObjectDTO;

public record AmenitiesDto(
    bool HasAirConditioning,
    bool HasHeating,
    bool IsFurnished,
    bool HasSwimmingPool,
    bool HasFireplace,
    bool HasGarden,
    bool HasSecuritySystem,
    bool? HasSmokingArea,
    bool HasParking
);