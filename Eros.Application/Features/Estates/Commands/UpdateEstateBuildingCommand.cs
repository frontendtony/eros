using Eros.Application.Features.Estates.Models;
using MediatR;

namespace Eros.Application.Features.Estates.Commands;

public sealed record UpdateEstateBuildingCommand
(
    Guid Id,
    Guid EstateId,
    string Name,
    string Description,
    string Address,
    Guid BuildingTypeId
) : IRequest<EstateBuildingResponseModel>;