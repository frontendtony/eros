using Eros.Api.Dto.Estates;
using ErrorOr;
using MediatR;

namespace Eros.Application.Features.Estates.Commands;

public record CreateEstateCommand(
    string Name,
    string Address,
    Guid UserId
) : IRequest<ErrorOr<CreateEstateCommandDto>>;
