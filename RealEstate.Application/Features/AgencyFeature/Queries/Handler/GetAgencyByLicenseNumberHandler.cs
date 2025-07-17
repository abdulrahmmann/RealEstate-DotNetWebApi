using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class GetAgencyByLicenseNumberHandler(IUnitOfWork unitOfWork): IRequestHandler<GetAgencyByLicenseNumberRequest, BaseResponse<AgencyDto>>
{
    #region INSTANCES
    private readonly IUnitOfWork  _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<AgencyDto>> Handle(GetAgencyByLicenseNumberRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.LicenseNumber))
            {
                return BaseResponse<AgencyDto>.NotFound("request cannot be null or empty");
            }

            var agency = await _unitOfWork.GetAgencyRepository.GetAgencyByLicenseNumber(request.LicenseNumber);
            
            if (agency is null)
            {
                return BaseResponse<AgencyDto>.NotFound();
            }
            
            var mapped = new AgencyDto(agency.Id, agency.Name, agency.LicenseNumber, agency.TaxNumber);
            
            return BaseResponse<AgencyDto>.Success(mapped);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<AgencyDto>.InternalError(e.Message);
        }
    }
}