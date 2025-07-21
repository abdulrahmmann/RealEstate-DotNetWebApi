using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Requests;

public record SearchPropertyByNameRequest(string Name): IRequest<BaseResponse<IQueryable<PropertyDto>>>;