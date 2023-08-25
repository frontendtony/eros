namespace Api.ResponseModels;

public class BaseResponseModel
{
    public int StatusCode { get; set; }
    public string? Message { get; set; } = string.Empty;
    public List<string> Messages { get; set; } = new List<string> { };
}

public class SingleResponseModel<T> : BaseResponseModel
{
    public required T? Data { get; set; } = default;
}

public class PaginatedResponseModel<T> : BaseResponseModel

{
    public required List<T> Data { get; set; }
    public required int PageSize { get; set; }
    public required int PageNumber { get; set; }
    public required int Total { get; set; }
}
