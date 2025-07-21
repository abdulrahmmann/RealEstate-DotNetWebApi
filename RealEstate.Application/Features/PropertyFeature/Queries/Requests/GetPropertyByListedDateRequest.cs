using MediatR;
using RealEstate.Application.Common;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Requests;

public record GetPropertyByListedDateRequest(DateOnly ListedDate): IRequest<BaseResponse<IQueryable<Property>>>;