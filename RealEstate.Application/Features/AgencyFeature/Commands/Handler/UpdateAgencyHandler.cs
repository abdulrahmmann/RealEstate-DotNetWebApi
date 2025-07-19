using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.Commands.Requests;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Mapping;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Commands.Handler;

public class UpdateAgencyHandler(IUnitOfWork unitOfWork, ILogger<Agency> logger): IRequestHandler<UpdateAgencyRequest, BaseResponse<Unit>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork; 
    private readonly ILogger<Agency> _logger = logger;
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(UpdateAgencyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check If The Request ID is Valid.
            if (request.Id <= 0)
            {
                return BaseResponse<Unit>.ValidationError("request Id cannot be less or equal than 0");
            }
            
            // 2. Check If The Agency By ID Is Exist. 
            var agency = await _unitOfWork.GetRepository<Agency>().GetByIdAsync(request.Id);

            // 3. Check If The Agency Is Null.
            if (agency is null)
            {
                return BaseResponse<Unit>.NotFound();
            }

            // 4. Map Agency 
            AgencyMapper.UpdateAgencyDto(agency, request.AgencyDto);
            
            // 5. Update Agency and SaveChanges.
            _unitOfWork.GetAgencyRepository.UpdateAgency(request.Id, agency);

            await _unitOfWork.SaveChangesAsync();
            
            // 6. Return.
            return BaseResponse<Unit>.Success();
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}
