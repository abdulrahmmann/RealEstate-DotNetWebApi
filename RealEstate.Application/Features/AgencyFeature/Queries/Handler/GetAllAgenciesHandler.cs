using FluentValidation;
using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;
using RealEstate.Application.Features.AgencyFeature.Queries.Requests;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.UOF;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Handler;

public class GetAllAgenciesHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<GetAllAgenciesRequest, BaseResponse<IEnumerable<AgencyDto>>>
{
    #region INSTANCES
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    #endregion

    public async Task<BaseResponse<IEnumerable<AgencyDto>>> Handle(GetAllAgenciesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var agencies = await _unitOfWork.GetRepository<Agency>().GetAllAsync();

            if (!agencies.Any())
            {
                return BaseResponse<IEnumerable<AgencyDto>>.NoContent();
            }

            var agenciesDto = agencies.Select(a => new AgencyDto(a.Id, a.Name, a.LicenseNumber, a.TaxNumber));

            var agenciesCount = await _unitOfWork.GetRepository<Agency>().CountAsync();
            
            return BaseResponse<IEnumerable<AgencyDto>>.Success(agenciesDto, "Agencies Retrieved Successful", agenciesCount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return BaseResponse<IEnumerable<AgencyDto>>.InternalError();
        }
    }
}