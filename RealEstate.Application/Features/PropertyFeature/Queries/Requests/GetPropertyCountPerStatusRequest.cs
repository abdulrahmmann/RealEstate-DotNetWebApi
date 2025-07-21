using MediatR;
using RealEstate.Application.Common;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Requests;

public record GetPropertyCountPerStatusRequest(): IRequest<BaseResponse<Dictionary<string, int>>>;