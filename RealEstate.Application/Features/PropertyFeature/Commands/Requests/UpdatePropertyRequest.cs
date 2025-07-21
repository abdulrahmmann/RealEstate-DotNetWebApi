using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;

namespace RealEstate.Application.Features.PropertyFeature.Commands.Requests;

public record UpdatePropertyRequest(int Id, UpdatePropertyDto PropertyDto): IRequest<BaseResponse<Unit>>;