using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.Commands.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Commands.Handler;

public class UpdateAgencyHandler(IUnitOfWork unitOfWork): IRequestHandler<UpdateAgencyRequest, BaseResponse<Unit>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork; 
    #endregion
    
    
    public async Task<BaseResponse<Unit>> Handle(UpdateAgencyRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var agency = await _unitOfWork.GetRepository<Agency>().GetByIdAsync(request.Id);

            if (agency is null)
            {
                return BaseResponse<Unit>.NotFound();
            }

            agency.Name = request.AgencyDto.Name ?? agency.Name;
            agency.LicenseNumber = request.AgencyDto.LicenseNumber ?? agency.LicenseNumber;
            agency.TaxNumber = request.AgencyDto.TaxNumber ?? agency.TaxNumber;
            
            _unitOfWork.GetAgencyRepository.UpdateAgency(request.Id, agency);

            await _unitOfWork.SaveChangesAsync();
            
            return BaseResponse<Unit>.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<Unit>.InternalError(e.Message);
        }
    }
}