using FluentValidation;
using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Repository;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Commands;

public class AddAgencyCommandHandler: IRequestHandler<AddAgencyCommand, BaseResponse<int>>
{
    #region Instance Fields
    private readonly IUnitOfWork  _unitOfWork;
    private readonly IValidator<AgencyDto> _validator;
    #endregion
    
    #region Constructor
    public AddAgencyCommandHandler(IUnitOfWork unitOfWork, IValidator<AgencyDto> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }
    #endregion
    
    public async Task<BaseResponse<int>> Handle(AddAgencyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var validationResult = await _validator.ValidateAsync(request.AgencyDto, cancellationToken);

            if (!validationResult.IsValid)
            {
                var validationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                
                return BaseResponse<int>.BadRequest($"Validation Error : {validationErrors}");
            }

            var agency = new Agency
            {
                Name = request.AgencyDto.Name,
                LicenseNumber = request.AgencyDto.LicenseNumber,
                TaxNumber = request.AgencyDto.TaxNumber
            };

            await _unitOfWork.GetRepository<Agency>().AddAsync(agency);
        
            await _unitOfWork.SaveChangesAsync();

            return BaseResponse<int>.Created("Agency Created Successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine("Add Agency Command Handler Error: " + e.Message);
            return BaseResponse<int>.InternalError();
        }
    }
}