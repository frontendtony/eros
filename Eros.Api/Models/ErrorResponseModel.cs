using FluentValidation.Results;

namespace Eros.Api.Models;

public class ErrorResponseModel : BaseResponseModel
{
    public IEnumerable<ValidationErrorModel>? ValidationErrors { get; set; }
}

public record ValidationErrorModel(string Field, string ErrorMessage);