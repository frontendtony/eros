using Eros.Application.Features.Users.Models;
using Eros.Application.Exceptions;
using Eros.Application.Features.Users.Commands;
using Eros.Application.Services;
using Eros.Domain.Aggregates.Users;
using FluentValidation;

namespace EstateManager.Handlers.CommandHandlers;

public class SignupCommandHandler
{
    private readonly ITokenService _tokenService;
    private readonly IUserWriteRepository _userWriteRepository;
    private readonly IUserReadRepository _userReadRepository;
    private readonly IValidator<SignupCommand> _validator;

    public SignupCommandHandler(
        ITokenService tokenService,
        IUserWriteRepository userWriteRepository,
        IUserReadRepository userReadRepository,
        IValidator<SignupCommand> validator
    )
    {
        _tokenService = tokenService;
        _userWriteRepository = userWriteRepository;
        _userReadRepository = userReadRepository;
        _validator = validator;
    }

    public async Task<SignupCommandResponse> Handle(SignupCommand request)
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

        var existingUser = await _userReadRepository.GetByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            throw new ConflictException($"A user address already exists with this email address: {request.Email}");
        }

        var user = User.Create(request.FirstName, request.LastName, request.Email, request.Avatar);

        var newUser = await _userWriteRepository.AddAsync(user, request.Password) ?? throw new BadRequestException("Error creating user");
        var token = _tokenService.GenerateToken(newUser);

        return new SignupCommandResponse(
            token,
            newUser.Email,
            newUser.FirstName,
            newUser.LastName,
            newUser.Avatar
        );
    }
}
