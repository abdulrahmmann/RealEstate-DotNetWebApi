using FluentValidation;
using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.Commands.Requests;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Commands.Handler;

public class AddAgencyHandler: IRequestHandler<AddAgencyRequest, BaseResponse<int>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddAgencyDto>  _validator;
    #endregion
    
    
    #region CONSTRUCTOR
    public AddAgencyHandler(IUnitOfWork unitOfWork, IValidator<AddAgencyDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    #endregion
    

    public async Task<BaseResponse<int>> Handle(AddAgencyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request.AgencyDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(',', validationResult.Errors.Select(err => err.ErrorMessage));
               return BaseResponse<int>.ValidationError(errors);
            }

            var req = request.AgencyDto;
            
            var agency = new Agency
            {
                Name = req.Name,
                LicenseNumber = req.LicenseNumber,
                TaxNumber = req.TaxNumber,
            };

            await _unitOfWork.GetRepository<Agency>().AddAsync(agency);

            await _unitOfWork.SaveChangesAsync();

            var totalCount = await _unitOfWork.GetRepository<Agency>().CountAsync();

            return BaseResponse<int>.Created(agency.Id, "Agency created successfully", totalCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<int>.InternalError(e.Message);
        }
    }
}