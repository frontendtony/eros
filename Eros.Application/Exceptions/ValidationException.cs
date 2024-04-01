using FluentValidation.Results;

namespace Eros.Application.Exceptions;

public class CustomValidationException(IEnumerable<ValidationFailure> errors) : Exception("Validation failed.")
{
    public IEnumerable<ValidationFailure> Errors { get; } = errors;
}
