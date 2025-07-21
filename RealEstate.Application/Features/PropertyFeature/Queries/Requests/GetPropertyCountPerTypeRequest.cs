using MediatR;
using RealEstate.Application.Common;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Requests;

public record GetPropertyCountPerTypeRequest(): IRequest<BaseResponse<Dictionary<string, int>>>;