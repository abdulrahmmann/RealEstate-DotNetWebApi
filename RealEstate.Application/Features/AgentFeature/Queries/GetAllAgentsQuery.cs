using MediatR;
using RealEstate.Application.Common;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public record GetAllAgentsQuery(): IRequest<BaseResponse<IEnumerable<>>>;