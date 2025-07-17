using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class GetAgencyByTaxNumberHandler(IUnitOfWork unitOfWork): IRequestHandler<GetAgencyByTaxNumberRequest, BaseResponse<AgencyDto>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion
    
    
    public async Task<BaseResponse<AgencyDto>> Handle(GetAgencyByTaxNumberRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.TaxNumber))
            {
                return BaseResponse<AgencyDto>.NotFound("request cannot be null or empty");
            }

            var agency = await _unitOfWork.GetAgencyRepository.GetAgencyByTaxNumber(request.TaxNumber);

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