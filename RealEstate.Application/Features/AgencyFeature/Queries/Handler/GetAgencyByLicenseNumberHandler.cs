using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Mapping;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class GetAgencyByLicenseNumberHandler(IUnitOfWork unitOfWork, ILogger<Agency> logger)
    : IRequestHandler<GetAgencyByLicenseNumberRequest, BaseResponse<AgencyDto>> 
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork  _unitOfWork = unitOfWork;
    private readonly ILogger<Agency> _logger = logger;
    #endregion
    
    
    public async Task<BaseResponse<AgencyDto>> Handle(GetAgencyByLicenseNumberRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check If The Request LicenseNumber Is Null or Empty.
            if (string.IsNullOrEmpty(request.LicenseNumber))
            {
                return BaseResponse<AgencyDto>.NotFound("request cannot be null or empty");
            }

            // 2. Get Agency By LicenseNumber.
            var agency = await _unitOfWork.GetAgencyRepository.GetAgencyByLicenseNumber(request.LicenseNumber);
            
            // 3. Check If Agency is Null.
            if (agency is null)
            {
                return BaseResponse<AgencyDto>.NotFound($"Agency with LicenseNumber: {request.LicenseNumber} was not found");
            }
            
            // 4. Map Agency to AgencyDTO.
            var mapped = agency.To_AgencyDto();
            
            // 5. Return Agency.
            return BaseResponse<AgencyDto>.Success(mapped, $"Agency with TaxNumber: {request.LicenseNumber} was successfully retrieved");
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<AgencyDto>.InternalError(e.Message);
        }
    }
}