using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.PropertyFeature.DTOs;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Features.PropertyFeature.Queries.Requests;

public record GetPropertyByPriceRequest(decimal FromPrice, decimal ToPrice): IRequest<BaseResponse<IQueryable<PropertyDto>>>;