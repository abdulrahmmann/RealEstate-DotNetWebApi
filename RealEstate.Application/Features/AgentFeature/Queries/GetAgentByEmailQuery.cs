using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public record GetAgentByEmailQuery(string Email): IRequest<BaseResponse<GetAgentDto>>;