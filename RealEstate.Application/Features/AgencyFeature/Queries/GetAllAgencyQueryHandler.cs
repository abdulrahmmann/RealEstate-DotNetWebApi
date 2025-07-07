using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries;

public class GetAllAgencyQueryHandler: IRequestHandler<GetAllAgencyQuery, BaseResponse<IEnumerable<AgencyDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    #endregion
    
    #region Constructor
    public GetAllAgencyQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion
    
    public async Task<BaseResponse<IEnumerable<AgencyDto>>> Handle(GetAllAgencyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var agencies = await _unitOfWork.GetRepository<Agency>().GetAllAsync();

            if (agencies is null || !agencies.Any())
            {
                return BaseResponse<IEnumerable<AgencyDto>>.BadRequest();
            }

            var agencyDto = agencies.Select(a => new AgencyDto(a.Name, a.LicenseNumber, a.TaxNumber));
            
            return BaseResponse<IEnumerable<AgencyDto>>.Success(agencyDto, "Agencies Retrieved Successfully", agencies.Count());
        }
        catch (Exception e)
        {
            Console.WriteLine("GetAllAgency Query Handler Error: " + e.Message);
            return BaseResponse<IEnumerable<AgencyDto>>.InternalError();
        }
    }
}