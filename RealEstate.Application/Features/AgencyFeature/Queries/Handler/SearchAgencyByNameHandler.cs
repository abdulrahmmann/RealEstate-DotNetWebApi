using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Mapping;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class SearchAgencyByNameHandler(IUnitOfWork unitOfWork, ILogger<Agency> logger):
    IRequestHandler<SearchAgencyByNameRequest, BaseResponse<IEnumerable<AgencyDto>>>
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork  _unitOfWork = unitOfWork;
    private readonly ILogger<Agency> _logger = logger;
    #endregion


    public Task<BaseResponse<IEnumerable<AgencyDto>>> Handle(SearchAgencyByNameRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check If The Request Name Is Null or Empty.
            if (string.IsNullOrEmpty(request.Name))
            {
                return Task.FromResult(BaseResponse<IEnumerable<AgencyDto>>.ValidationError("request cannot be null or empty"));
            }

            // 2. Get All Searched Agencies.
            var agencies = _unitOfWork.GetAgencyRepository.SearchAgencyByName(request.Name);
            
            // 3. Materialize  and Check if Agencies is Empty.
            var agenciesList = agencies.ToList();
            
            if (!agenciesList.Any())
            {
                return Task.FromResult(BaseResponse<IEnumerable<AgencyDto>>.NoContent());
            }

            // 4. Map Agency to AgencyDTO.
            var mapped = agenciesList.To_AgencyDto_List();
            
            // 5. Return Agencies.
            return Task.FromResult(BaseResponse<IEnumerable<AgencyDto>>.Success(mapped));
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return Task.FromResult(BaseResponse<IEnumerable<AgencyDto>>.InternalError(e.Message));
        }
    }
}