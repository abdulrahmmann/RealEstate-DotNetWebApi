using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;

namespace RealEstate.Application.Features.AgencyFeature.Queries;

public record GetAgencyByIdQuery(int Id): IRequest<BaseResponse<AgencyDto>>;