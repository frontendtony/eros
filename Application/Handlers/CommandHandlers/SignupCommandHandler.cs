using Api.ResponseModels;
using Application.Exceptions;
using EstateManager.Commands;
using EstateManager.Interfaces;
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

    public async Task<SingleResponseModel<SignupResponseModel>> Handle(SignupCommand request)
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

        if (user is not null)
        {
            throw new ConflictException($"A user address already exists with this email address: {request.Email}");
        }

        var newUser = await _userWriteRepository.CreateUserAsync(request) ?? throw new BadRequestException("Error creating user");
        var token = _tokenService.GenerateToken(newUser);

        return new SingleResponseModel<SignupResponseModel>
        {
            Data = new SignupResponseModel(newUser, token),
            StatusCode = StatusCodes.Status201Created,
            Message = "User created successfully"
        };
    }
}
