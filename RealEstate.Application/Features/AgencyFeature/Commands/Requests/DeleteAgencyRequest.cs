using MediatR;
using RealEstate.Application.Common;

namespace RealEstate.Application.Features.AgencyFeature.Commands.Requests;

public record DeleteAgencyRequest(int Id): IRequest<BaseResponse<Unit>>;