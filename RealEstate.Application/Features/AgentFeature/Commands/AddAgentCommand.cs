using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Commands;

public record AddAgentCommand(AddAgentDto AgentDto): IRequest<BaseResponse<int>>;