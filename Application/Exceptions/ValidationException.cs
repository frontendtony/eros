namespace Eros.Application.Exceptions;

public class CustomValidationException : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; }

    public CustomValidationException(IEnumerable<ValidationFailure> errors)
        : base("Validation failed.")
    {
        Errors = errors;
    }
}

public class ValidationFailure
{
    public string ErrorMessage { get; set; } = string.Empty;
}
