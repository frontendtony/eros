using Eros.Application.Abstractions;
using Eros.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Eros.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommandBase
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (validator is null)
        {
            return await next();
        }

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        throw new CustomValidationException(validationResult.Errors);
    }
}
