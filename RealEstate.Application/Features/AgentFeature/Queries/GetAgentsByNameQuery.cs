using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public record GetAgentsByNameQuery(string Name): IRequest<BaseResponse<IQueryable<GetAgentDto>>>;