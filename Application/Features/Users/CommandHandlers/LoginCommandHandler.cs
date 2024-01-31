using FluentValidation;
using Eros.Application.Features.Users.Commands;
using Eros.Application.Exceptions;
using Eros.Domain.Aggregates.Users;
using Eros.Application.Services;
using Application.Features.Users.Models;

namespace Eros.Application.Features.Users.CommandHandlers;

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

    public async Task<LoginResponse> Handle(LoginCommand request)
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

        var user = await _userReadRepository.GetByEmailAsync(request.Email);

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

        return new LoginResponse(token);
    }
}
