namespace Eros.Application.Exceptions;

public class CustomValidationException(IEnumerable<ValidationFailure> errors) : Exception("Validation failed.")
{
    public IEnumerable<ValidationFailure> Errors { get; } = errors;
}

public class ValidationFailure
{
    public string ErrorMessage { get; set; } = string.Empty;
}
