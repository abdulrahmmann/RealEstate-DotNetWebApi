using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Mapping;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class GetAgencyByIdRequestHandler(IUnitOfWork unitOfWork, ILogger<Agency> logger)
    : IRequestHandler<GetAgencyByIdRequest, BaseResponse<AgencyDto>>
{
    #region Create Instance and Inject it into Primary Constructor.
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<Agency> _logger = logger;
    #endregion
    
    public async Task<BaseResponse<AgencyDto>> Handle(GetAgencyByIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check If The Request ID Is Valid.
            if (request.Id <= 0)
            {
                return BaseResponse<AgencyDto>.ValidationError("Id cannot be less than or equal to zero");
            }

            // 2. Get Agency by Id.
            var agency = await _unitOfWork.GetRepository<Agency>().GetByIdAsync(request.Id);

            // 3. Check If The Agency is Null.
            if (agency is null)
            {
                return BaseResponse<AgencyDto>.NotFound($"Agency with Id: {request.Id} was not found");
            }

            // 4. Map Agency to AgencyDTO.
            var mapped = agency.To_AgencyDto();
            
            // 5. Return Agency.
            return BaseResponse<AgencyDto>.Success(mapped, $"Agency with Id: {request.Id} was successfully retrieved");
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<AgencyDto>.InternalError(e.Message);
        }
    }
}