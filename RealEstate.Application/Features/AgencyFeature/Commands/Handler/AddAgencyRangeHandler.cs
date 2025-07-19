using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.Commands.Requests;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Commands.Handler;

public class AddAgencyRangeHandler: IRequestHandler<AddAgencyRangeRequest, BaseResponse<int>>
{
    #region Create Instances.
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddAgencyDto>  _validator;
    private readonly ILogger<Agency>  _logger;
    #endregion
    
    #region Inject Instances into Constructor.
    public AddAgencyRangeHandler(IUnitOfWork unitOfWork, IValidator<AddAgencyDto> validator, ILogger<Agency> logger)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }
    #endregion
    
    
    public async Task<BaseResponse<int>> Handle(AddAgencyRangeRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Create List Of Categories to Add Just The Valid Items.
            var invalidItems = new List<string>();
            
            var validItems = new List<Agency>();

            // 2. Iterate to The List.
            foreach (var dto in request.AgenciesDto)
            {
                // 3. Check If The Agency Is Exist.
                var existingAgency =
                    await _unitOfWork.Context.Agencies.FirstOrDefaultAsync(c => c.Name == dto.Name, cancellationToken);
                    
                if (existingAgency != null)
                {
                    continue;
                }
                else
                {
                    // 4. Check Validation Is Valid.
                    var validationResult = await _validator.ValidateAsync(dto, cancellationToken);

                    if (!validationResult.IsValid)
                    {
                        var errors = string.Join(", ", validationResult.Errors.Select(err => err.ErrorMessage));
                        invalidItems.Add($"[{dto.Name}]: {errors}");
                    }
                
                    validItems.Add(new Agency
                    {
                        Name = dto.Name,   
                        LicenseNumber = dto.LicenseNumber,
                        TaxNumber = dto.TaxNumber,
                    });
                }
            }

            if (validItems.Count != 0)
            {
                await _unitOfWork.GetRepository<Agency>().AddRangeAsync(validItems);
                await _unitOfWork.SaveChangesAsync();
            }
            
            var totalCount = await _unitOfWork.GetRepository<Agency>().CountAsync();
            
            if (invalidItems.Any() && validItems.Any())
            {
                return BaseResponse<int>.Created(
                    validItems.Count,
                    $"Some agencies were added, but others failed validation: {string.Join(" | ", invalidItems)}",
                    totalCount
                );
            }
            
            if (!validItems.Any())
            {
                return BaseResponse<int>.ValidationError($"No agencies were added. Errors: {string.Join(" | ", invalidItems)}");
            }
            
            return BaseResponse<int>.Created(validItems.Count, "All agencies created successfully.", totalCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<int>.InternalError(e.Message);
        }
    }
}