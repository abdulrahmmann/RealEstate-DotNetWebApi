using MediatR;
using RealEstate.Application.Common;

namespace RealEstate.Application.Features.PropertyFeature.Commands.Requests;

public record DeletePropertyRequest(int Id): IRequest<BaseResponse<Unit>>;