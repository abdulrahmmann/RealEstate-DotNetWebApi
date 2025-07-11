using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public record GetAgentByIdQuery(int Id): IRequest<BaseResponse<GetAgentDto>>;