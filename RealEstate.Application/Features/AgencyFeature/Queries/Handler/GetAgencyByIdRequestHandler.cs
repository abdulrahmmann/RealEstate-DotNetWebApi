using FluentValidation;
using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class GetAgencyByIdRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAgencyByIdRequest, BaseResponse<AgencyDto>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    public async Task<BaseResponse<AgencyDto>> Handle(GetAgencyByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id <= 0)
            {
                return BaseResponse<AgencyDto>.ValidationError("Id cannot be less than or equal to zero");
            }

            var agency = await _unitOfWork.GetRepository<Agency>().GetByIdAsync(request.Id);

            if (agency.Equals(null))
            {
                return BaseResponse<AgencyDto>.NotFound($"Agency with Id: {request.Id} was not found");
            }

            var mapped = new AgencyDto(agency.Id, agency.Name, agency.LicenseNumber, agency.TaxNumber);
            
            return BaseResponse<AgencyDto>.Success(mapped, $"Agency with Id: {request.Id} was successfully retrieved");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<AgencyDto>.InternalError(e.Message);
        }
    }
}