using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.AgencyFeature.DTOs;

namespace RealEstate.Application.Features.AgencyFeature.Commands.Requests;

public record AddAgencyRequest(AddAgencyDto AgencyDto): IRequest<BaseResponse<int>>;