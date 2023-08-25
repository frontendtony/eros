using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Interfaces;

namespace EstateManager.Handlers.QueryHandlers;

public class GetUserQueryHandler : EstateManagerBaseHandler
{
    private readonly IUserReadRepository _userReadRepository;

    public GetUserQueryHandler(IUserReadRepository userReadRepository, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
    {
        _userReadRepository = userReadRepository;
    }

    public async Task<SingleResponseModel<UserResponseModel>> Handle(Guid userId)
    {
        if (IsSelf(userId) || IsAdmin())
        {
            var user = await _userReadRepository.GetUserByIdAsync(userId.ToString()) ?? throw new NotFoundException("User not found");

            return new SingleResponseModel<UserResponseModel>
            {
                Data = new UserResponseModel(user),
                StatusCode = StatusCodes.Status200OK
            };
        }
        else
        {
            throw new ForbiddenException("You are not allowed to get other users");
        }
    }
}
