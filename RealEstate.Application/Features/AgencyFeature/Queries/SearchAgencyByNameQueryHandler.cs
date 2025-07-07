using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries;

public class SearchAgencyByNameQueryHandler: IRequestHandler<SearchAgencyByNameQuery, BaseResponse<IQueryable<AgencyDto>>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    #endregion
    
    #region Constructor
    public SearchAgencyByNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    #endregion
    
    public async Task<BaseResponse<IQueryable<AgencyDto>>> Handle(SearchAgencyByNameQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var agencies = _unitOfWork.GetAgencyRepository.SearchAgencyByName(request.Name);
            
            if (agencies is null || !agencies.Any())
            {
                return BaseResponse<IQueryable<AgencyDto>>.BadRequest("No agencies found matching the name.");
            }

            var agenciesDto = agencies.Select(a => new AgencyDto(a.Name, a.LicenseNumber, a.TaxNumber));
            
            var totalCount = await _unitOfWork.GetRepository<Agency>().GetTotalCountAsync();
            
            return BaseResponse<IQueryable<AgencyDto>>.Success(agenciesDto, "Searched Agencies Retrieved Successfully", totalCount);
        }
        catch (Exception e)
        {
            Console.WriteLine("SearchAgencyByName Query Handler Error: " + e.Message);
            return BaseResponse<IQueryable<AgencyDto>>.InternalError();
        }
    }
}