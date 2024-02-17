using Eros.Application.Features.Estates.Models;
using MediatR;

namespace Eros.Application.Features.Estates.Queries;

public record GetEstateByIdQuery(Guid Id) : IRequest<EstateResponseModel>;