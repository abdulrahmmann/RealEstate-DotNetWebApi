using MediatR;
using RealEstate.Application.Common;

namespace RealEstate.Application.Features.AgentFeature.Commands.Requests;

public record DeleteAgentRequest(int Id): IRequest<BaseResponse<Unit>>;