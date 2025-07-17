using MediatR;
using RealEstate.Application.Common;
using RealEstate.Application.Features.CategoryFeature.DTOs;

namespace RealEstate.Application.Features.CategoryFeature.Commands.Requests;

public record AddCategoryRangeRequest(IEnumerable<AddCategoryDto> CategoriesDto): IRequest<BaseResponse<Unit>>;