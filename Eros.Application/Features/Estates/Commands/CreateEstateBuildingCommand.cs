using Eros.Application.Features.Estates.Models;
using MediatR;

namespace Eros.Application.Features.Estates.Commands;

public sealed record CreateEstateBuildingCommand
(
    string Name,
    string Description,
    string Address,
    Guid EstateId,
    Guid BuildingTypeId
) : IRequest<EstateBuildingResponseModel>;