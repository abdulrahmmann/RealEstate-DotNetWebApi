using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class SearchAgencyByNameHandler(IUnitOfWork unitOfWork): IRequestHandler<SearchAgencyByNameRequest, BaseResponse<IQueryable<AgencyDto>>>
{
    #region INSTANCES
    private readonly IUnitOfWork  _unitOfWork = unitOfWork;
    #endregion
    
    public async Task<BaseResponse<IQueryable<AgencyDto>>> Handle(SearchAgencyByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return BaseResponse<IQueryable<AgencyDto>>.ValidationError("request cannot be null or empty");
            }

            var agencies = _unitOfWork.GetAgencyRepository.SearchAgencyByName(request.Name);

            if (!agencies.Any())
            {
                return BaseResponse<IQueryable<AgencyDto>>.NotFound();
            }

            var mapped = agencies.Select(a => new AgencyDto(a.Id, a.Name, a.LicenseNumber, a.TaxNumber));

            return BaseResponse<IQueryable<AgencyDto>>.Success(mapped);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IQueryable<AgencyDto>>.InternalError(e.Message);
        }
    }
}