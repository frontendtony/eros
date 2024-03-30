using Eros.Application.Features.Estates.Models;
using MediatR;

namespace Eros.Application.Features.Estates.Commands;

public record DeleteEstateCommand(
    Guid Id
) : IRequest;