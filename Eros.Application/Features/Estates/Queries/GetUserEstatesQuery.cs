using Eros.Api.Dto.Estates;
using MediatR;

namespace Eros.Application.Features.Estates.Queries;

public record GetUserEstatesQuery(Guid UserId) : IRequest<List<EstateDto>>;
