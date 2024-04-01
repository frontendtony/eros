namespace Eros.Api.Models;

public class SingleResponseModel<T> : BaseResponseModel
{
    public required T? Data { get; init; } = default;
}