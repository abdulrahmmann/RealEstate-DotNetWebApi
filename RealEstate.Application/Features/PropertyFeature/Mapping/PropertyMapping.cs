using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.DTOs.ValueObjectDTO;
using RealEstate.Domain.Entities;
using RealEstate.Domain.ValueObjects;

namespace RealEstate.Application.Features.PropertyFeature.Mapping;

public static class PropertyMapping
{
    #region Map From Property Entity To Property DTO.
    public static PropertyDto To_PropertyDto(this Property property)
    {
        return new PropertyDto(
            property.Id,
            property.Name,
            property.Description,
            property.Type,
            property.Status,
            property.Price,
            property.Rating,
            property.ListedDate,
            property.ImageUrls,

            (property.Address is not null
                ? new AddressDto(
                    property.Address.Country,
                    property.Address.City,
                    property.Address.Street,
                    property.Address.ZipCode
                ) : null)!,

            (property.Amenities is not null
                ? new AmenitiesDto(
                    property.Amenities.HasAirConditioning,
                    property.Amenities.HasHeating,
                    property.Amenities.IsFurnished,
                    property.Amenities.HasSwimmingPool,
                    property.Amenities.HasFireplace,
                    property.Amenities.HasGarden,
                    property.Amenities.HasSecuritySystem,
                    property.Amenities.HasSmokingArea,
                    property.Amenities.HasParking
                ) : null)!,

            (property.Facilities is not null
                ? new FacilitiesDto(
                    property.Facilities.Area,
                    property.Facilities.Rooms,
                    property.Facilities.Kitchens,
                    property.Facilities.Balconies,
                    property.Facilities.Baths,
                    property.Facilities.Beds
                ) : null)!,

            property.Category?.Name ?? string.Empty,
            property.Agent?.Name ?? string.Empty
        );
    }

    public static IEnumerable<PropertyDto> To_PropertyDto_List(this IEnumerable<Property> properties)
    {
        return properties.Select(p => p.To_PropertyDto());
    }
    #endregion
    
    
    #region Map From Property DTO To Property Entity.
    public static Property To_PropertyEntity(this AddPropertyDto req, Agent agent, Category category)
    {
        return new Property
        {
            Name = req.Name,
            Description = req.Description,
            Type = req.Type,
            Status = req.Status,
            Price = req.Price,
            Rating = req.Rating,
            ListedDate = req.ListedDate,
            ImageUrls = req.ImageUrls,
            CategoryId = category.Id,
            AgentId = agent.Id,

            Address = new Address(
                req.Address.Country,
                req.Address.City,
                req.Address.Street,
                req.Address.ZipCode
            ),

            Amenities = new Amenities(
                req.Amenities.HasAirConditioning,
                req.Amenities.HasHeating,
                req.Amenities.IsFurnished,
                req.Amenities.HasSwimmingPool,
                req.Amenities.HasFireplace,
                req.Amenities.HasGarden,
                req.Amenities.HasSecuritySystem,
                req.Amenities.HasSmokingArea,
                req.Amenities.HasParking
            ),

            Facilities = new Facilities(
                req.Facilities.Area,
                req.Facilities.Rooms,
                req.Facilities.Kitchens,
                req.Facilities.Balconies,
                req.Facilities.Baths,
                req.Facilities.Beds
            )
        };
    }
    #endregion
    
    
    
    public static IQueryable<PropertyDto> To_PropertyDto_Queryable(this IEnumerable<Property> properties)
    {
        return properties.Select(p => p.To_PropertyDto()).AsQueryable();
    }
    
    
}