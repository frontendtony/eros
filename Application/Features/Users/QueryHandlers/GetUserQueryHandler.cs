using Eros.Application.Exceptions;
using Eros.Application.Features.Users.Models;
using Eros.Application.Features.Users.Queries;
using Eros.Domain.Aggregates.Users;
using Eros.Handlers;

namespace Eros.Application.Features.Users.QueryHandlers;

public class GetUserQueryHandler : BaseHandler
{
    private readonly IUserReadRepository _userReadRepository;

    public GetUserQueryHandler(IUserReadRepository userReadRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<GetUserResponseModel> Handle(GetUserQuery query)
    {
        if (IsSelf(query.UserId) || IsAdmin())
        {
            var user = await _userReadRepository.GetByIdAsync(query.UserId.ToString()) ?? throw new NotFoundException("User not found");

            return new GetUserResponseModel(user);
        }
        else
        {
            throw new ForbiddenException("You are not allowed to get other users");
        }
    }
}
