using Eros.Application.Exceptions;
using Eros.Application.Features.Users.Models;
using Eros.Application.Features.Users.Queries;
using Eros.Domain.Aggregates.Users;
using Microsoft.AspNetCore.Http;

namespace Eros.Application.Features.Users.QueryHandlers;

public class GetUserQueryHandler(IUserReadRepository userReadRepository, IHttpContextAccessor httpContextAccessor)
    : ErosBaseHandler(httpContextAccessor)
{
    public async Task<GetUserQueryResponse> Handle(GetUserQuery query)
    {
        if (IsSelf(query.UserId) || IsAdmin())
        {
            var user = await userReadRepository.GetByIdAsync(query.UserId.ToString()) ?? throw new NotFoundException("User not found");

            return new GetUserQueryResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
        else
        {
            throw new ForbiddenException("You are not allowed to get other users");
        }
    }
}
