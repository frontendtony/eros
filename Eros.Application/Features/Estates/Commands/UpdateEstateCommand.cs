using Eros.Application.Features.Estates.Models;
using MediatR;

namespace Eros.Application.Features.Estates.Commands;

public record UpdateEstateCommand(
    Guid Id,
    string Name,
    string Address,
    string? LatLng
) : IRequest<EstateResponseModel>;