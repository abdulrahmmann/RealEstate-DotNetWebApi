using MediatR;
using RealEstate.Application.Common;

namespace RealEstate.Application.Features.CategoryFeature.Commands.Requests;

public record DeleteCategoryRequest(int Id): IRequest<BaseResponse<Unit>>;