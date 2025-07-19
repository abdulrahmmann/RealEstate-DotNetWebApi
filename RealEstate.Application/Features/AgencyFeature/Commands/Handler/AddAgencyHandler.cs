using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.Commands.Requests;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;
using RealEstate.Application.Features.AgencyFeature.Mapping;


namespace RealEstate.Application.Features.AgencyFeature.Commands.Handler;

public class AddAgencyHandler: IRequestHandler<AddAgencyRequest, BaseResponse<int>>
{
    #region Create Instances.
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddAgencyDto>  _validator;
    private readonly ILogger<Agency>  _logger;
    #endregion
    
    
    #region Inject Instances into Constructor.
    public AddAgencyHandler(IUnitOfWork unitOfWork, IValidator<AddAgencyDto> validator, ILogger<Agency> logger)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }
    #endregion
    

    public async Task<BaseResponse<int>> Handle(AddAgencyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Check Request Validation.
            var validationResult = await _validator.ValidateAsync(request.AgencyDto, cancellationToken);

            // 2. If Is Not Valid, return Errors.
            if (!validationResult.IsValid)
            {
                var errors = string.Join(',', validationResult.Errors.Select(err => err.ErrorMessage));
               return BaseResponse<int>.ValidationError(errors);
            }

            var req = request.AgencyDto;

            // 3. Map Agency To AddAgencyDTO.
            var agency = req.To_AddAgencyDto();

            // 4. Add Agency and SaveChanges;
            await _unitOfWork.GetRepository<Agency>().AddAsync(agency);

            await _unitOfWork.SaveChangesAsync();

            // 5. Get Agency Count.
            var totalCount = await _unitOfWork.GetRepository<Agency>().CountAsync();

            // 6. Return Created Response.
            return BaseResponse<int>.Created(agency.Id, "Agency created successfully", totalCount);
        }
        catch (Exception e)
        {
            _logger.LogError("Internal Server Error: {EMessage}", e.Message);
            return BaseResponse<int>.InternalError(e.Message);
        }
    }
}