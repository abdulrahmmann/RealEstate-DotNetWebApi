using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgentFeature.DTOs;

namespace RealEstate.Application.Features.AgentFeature.Queries;

public record GetAgentByPhoneQuery(string PhoneNumber): IRequest<BaseResponse<GetAgentDto>>;