using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Queries.Requests;

public record GetAgentByEmailRequest(string Email): IRequest<BaseResponse<AgentDto>>;