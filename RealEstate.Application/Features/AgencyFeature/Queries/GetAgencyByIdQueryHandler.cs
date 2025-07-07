using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries;

public class GetAgencyByIdQueryHandler: IRequestHandler<GetAgencyByIdQuery, BaseResponse<AgencyDto>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    #endregion
    
    #region Constructor
    public GetAgencyByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion
    
    public async Task<BaseResponse<AgencyDto>> Handle(GetAgencyByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var agency = await _unitOfWork.GetRepository<Agency>().GetByIdAsync(request.Id);
            
            if (agency is null)
            {
                return BaseResponse<AgencyDto>.BadRequest();
            }

            var agencyDto = new AgencyDto(agency.Name, agency.LicenseNumber, agency.TaxNumber);

            return BaseResponse<AgencyDto>.Success(agencyDto, $"Agency By Id: {request.Id} Retrieved Successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine("GetAgencyById Query Handler Error: " + e.Message);
            return BaseResponse<AgencyDto>.InternalError();
        }
    }
}