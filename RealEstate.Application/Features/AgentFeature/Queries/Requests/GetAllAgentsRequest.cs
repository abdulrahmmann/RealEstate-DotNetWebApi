using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Queries.Requests;

public record GetAllAgentsRequest(): IRequest<BaseResponse<IEnumerable<AgentDto>>>;