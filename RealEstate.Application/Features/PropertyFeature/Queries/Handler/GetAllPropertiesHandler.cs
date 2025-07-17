using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Application.Features.PropertyFeature.DTOs.ValueObjectDTO;
using RealEstate.Application.Features.PropertyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Handler;

public class GetAllPropertiesHandler(IUnitOfWork unitOfWork): IRequestHandler<GetAllPropertiesRequest, BaseResponse<IEnumerable<PropertyDto>>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    public async Task<BaseResponse<IEnumerable<PropertyDto>>> Handle(GetAllPropertiesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var properties = await _unitOfWork.GetPropertyRepository.GetAllPropertiesAsync();
            
            if (properties == null) return BaseResponse<IEnumerable<PropertyDto>>.NotFound();
            
            if (!properties.Any()) return BaseResponse<IEnumerable<PropertyDto>>.NoContent();

            var mapped = properties.Select(p => new PropertyDto(
                Id: p.Id,
                Name: p.Name,
                Description: p.Description,
                Type: p.Type,
                Status: p.Status,
                Price: p.Price,
                Rating: p.Rating,
                ListedDate: p.ListedDate,
                ImageUrls: p.ImageUrls,

                Address: p.Address == null ? null : new AddressDto(
                    Country: p.Address.Country,
                    City: p.Address.City,
                    Street: p.Address.Street,
                    ZipCode: p.Address.ZipCode
                ),

                Amenities: p.Amenities == null ? null : new AmenitiesDto(
                    HasAirConditioning: p.Amenities.HasAirConditioning,
                    HasHeating: p.Amenities.HasHeating,
                    IsFurnished: p.Amenities.IsFurnished,
                    HasSwimmingPool: p.Amenities.HasSwimmingPool,
                    HasFireplace: p.Amenities.HasFireplace,
                    HasGarden: p.Amenities.HasGarden,
                    HasSecuritySystem: p.Amenities.HasSecuritySystem,
                    HasSmokingArea: p.Amenities.HasSmokingArea,
                    HasParking: p.Amenities.HasParking
                ),

                Facilities: p.Facilities == null ? null : new FacilitiesDto(
                    Area: p.Facilities.Area,
                    Rooms: p.Facilities.Rooms,
                    Kitchens: p.Facilities.Kitchens,
                    Balconies: p.Facilities.Balconies,
                    Baths: p.Facilities.Baths,
                    Beds: p.Facilities.Beds
                ),

                CategoryName: p.Category?.Name,
                AgentName: p.Agent?.Name 
            ));


            return BaseResponse<IEnumerable<PropertyDto>>.Success(mapped);

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IEnumerable<PropertyDto>>.InternalError(e.Message);
        }
    }
}