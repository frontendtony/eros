using Eros.Api.Dto.Estates;
using Eros.Application.Features.Estates.Queries;
using Eros.Domain.Aggregates.Users;
using Mapster;
using MediatR;

namespace Eros.Application.Features.Estates.QueryHandlers;

public class GetUserEstatesQueryHandler(
  IUserReadRepository userReadRepository
) : IRequestHandler<GetUserEstatesQuery, List<EstateDto>>
{
  public async Task<List<EstateDto>> Handle(GetUserEstatesQuery request, CancellationToken cancellationToken)
  {
    var estates = await userReadRepository.GetEstatesAsync(request.UserId, cancellationToken);

    var response = estates.Select(estate => estate.Adapt<EstateDto>()).ToList();

    return response;
  }
}
