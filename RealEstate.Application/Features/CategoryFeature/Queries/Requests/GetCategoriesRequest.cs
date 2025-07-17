using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.CategoryFeature.DTOs;

namespace RealEstate.Application.Features.CategoryFeature.Queries.Requests;

public record GetCategoriesRequest(): IRequest<BaseResponse<IEnumerable<CategoryDto>>>;