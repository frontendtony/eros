namespace Eros.Api.Models;

public class BaseResponseModel
{
    public int StatusCode { get; set; }
    public string? Message { get; set; } = string.Empty;
    public List<string> ValidationErros { get; set; } = new List<string> { };
}
