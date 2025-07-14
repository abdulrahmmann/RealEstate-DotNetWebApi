using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Requests;

public record GetAllAgenciesRequest(): IRequest<BaseResponse<IEnumerable<AgencyDto>>>;