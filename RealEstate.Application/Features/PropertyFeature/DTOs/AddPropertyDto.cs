using RealEstate.Application.Features.PropertyFeature.DTOs.ValueObjectDTO;
using RealEstate.Domain.Enums;

namespace RealEstate.Application.Features.PropertyFeature.DTOs;

public record AddPropertyDto(
    int Id,
    string Name,
    string Description,
    PropertyType Type,
    PropertyStatus Status,
    decimal Price,
    double? Rating,
    DateTime ListedDate,
    List<string> ImageUrls,
    AddressDto Address,
    AmenitiesDto Amenities,
    FacilitiesDto Facilities,
    string CategoryName,
    string AgentName
);