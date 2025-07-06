using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;

namespace RealEstate.Application.Features.AgencyFeature.Commands;

public record AddAgencyCommand(AgencyDto AgencyDto): IRequest<BaseResponse<int>>;