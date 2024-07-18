using Eros.Api.Dto.Auth;
using Eros.Application.Abstractions;

namespace Eros.Application.Features.Auth.Commands;

public sealed record RefreshTokenCommand(string RefreshToken, Guid UserId) : ICommand<RefreshTokenResponseDto>;
