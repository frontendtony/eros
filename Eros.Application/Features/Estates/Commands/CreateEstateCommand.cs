using Eros.Api.Dto.Estates;
using Eros.Application.Abstractions;

namespace Eros.Application.Features.Estates.Commands;

public record CreateEstateCommand(
    string Name,
    string Address,
    Guid UserId
) : ICommand<CreateEstateCommandDto>;
