using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Commands.Requests;

public record AddAgentRangeRequest(IEnumerable<AddAgentDto> AgentsDto): IRequest<BaseResponse<int>>;