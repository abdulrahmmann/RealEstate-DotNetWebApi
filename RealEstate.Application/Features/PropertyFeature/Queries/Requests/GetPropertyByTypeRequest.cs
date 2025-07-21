using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Enums;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Requests;

public record GetPropertyByTypeRequest(string Type): IRequest<BaseResponse<IQueryable<PropertyDto>>>;