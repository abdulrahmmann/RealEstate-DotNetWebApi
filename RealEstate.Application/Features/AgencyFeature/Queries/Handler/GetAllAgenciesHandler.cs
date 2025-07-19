using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Mapping;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class GetAllAgenciesHandler(IUnitOfWork unitOfWork, ILogger<Agency> logger) 
    : IRequestHandler<GetAllAgenciesRequest, BaseResponse<IEnumerable<AgencyDto>>>
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Agency> _logger = logger;
    #endregion

    public async Task<BaseResponse<IEnumerable<AgencyDto>>> Handle(GetAllAgenciesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Get All Agencies.
            var agencies = await _unitOfWork.GetRepository<Agency>().GetAllAsync();

            // 2. Check If Agencies Is Empty.
            var agenciesList = agencies.ToList();
            if (!agenciesList.Any())
            {
                return BaseResponse<IEnumerable<AgencyDto>>.NoContent();
            }

            // 3. Map Agencies To AgenciesDTOs.
            var agenciesDto = agenciesList.To_AgencyDto_List();

            // 4. Get Agencies Count.
            var agenciesCount = agenciesList.Count;
            
            // 5. Return Agencies.
            return BaseResponse<IEnumerable<AgencyDto>>.Success(agenciesDto, "Agencies Retrieved Successful", agenciesCount);
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<IEnumerable<AgencyDto>>.InternalError();
        }
    }
}