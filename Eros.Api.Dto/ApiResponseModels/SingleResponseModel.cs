namespace Eros.Api.Dto.ApiResponseModels;

public class SingleResponseModel<T> : BaseResponseModel
{
    public required T? Data { get; init; } = default;
}