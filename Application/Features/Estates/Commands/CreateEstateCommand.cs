using Eros.Application.Features.Estates.Models;
using MediatR;

namespace Eros.Application.Features.Estates.Commands;

public record CreateEstateCommand(
    string Name,
    string Address,
    string? LatLng
) : IRequest<EstateResponseModel>
{
}
