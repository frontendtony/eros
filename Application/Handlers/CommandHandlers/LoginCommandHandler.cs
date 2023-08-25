using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Commands;
using EstateManager.Interfaces;
using FluentValidation;

namespace EstateManager.Handlers.CommandHandlers;

public class LoginCommandHandler
{
    private readonly IUserReadRepository _userReadRepository;
    private readonly ITokenService _tokenService;
    private readonly IValidator<LoginCommand> _validator;

    public LoginCommandHandler(
        ITokenService tokenService,
        IUserReadRepository userReadRepository,
        IValidator<LoginCommand> validator
    )
    {
        _userReadRepository = userReadRepository;
        _tokenService = tokenService;
        _validator = validator;
    }

    public async Task<SingleResponseModel<string>> Handle(LoginCommand request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new CustomValidationException(validationResult.Errors.Select(x => new ValidationFailure()
            {
                ErrorMessage = x.ErrorMessage
            })
            .ToList());
        }

        var user = await _userReadRepository.GetUserByEmailAsync(request.Email);

        if (user is null)
        {
            throw new BadRequestException("Incorrect email address");
        }


        if (user.IsAdmin)
        {
            throw new ForbiddenException("User is an admin");
        }

        var isPasswordValid = await _userReadRepository.CheckPassword(user, request.Password);

        if (!isPasswordValid)
        {
            throw new BadRequestException("Password is invalid");
        }

        var token = _tokenService.GenerateToken(user);

        return new SingleResponseModel<string>
        {
            Data = token,
            StatusCode = StatusCodes.Status200OK
        };
    }
}
