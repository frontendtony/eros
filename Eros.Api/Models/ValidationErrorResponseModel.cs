namespace Eros.Api.Models;

public class ValidationErrorResponseModel : BaseResponseModel
{
    string[] ValidationErrors { get; set; } = Array.Empty<string>();
}