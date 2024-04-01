using FluentValidation.Results;

namespace Eros.Api.Models;

public class ErrorResponseModel : BaseResponseModel
{
    public IEnumerable<ValidationFailure>? ValidationErrors { get; set; }
}