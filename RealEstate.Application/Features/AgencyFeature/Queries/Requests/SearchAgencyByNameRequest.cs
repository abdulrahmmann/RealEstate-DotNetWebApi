using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;

namespace RealEstate.Application.Features.AgencyFeature.Queries.Requests;

public record SearchAgencyByNameRequest(string Name): IRequest<BaseResponse<IQueryable<AgencyDto>>>;