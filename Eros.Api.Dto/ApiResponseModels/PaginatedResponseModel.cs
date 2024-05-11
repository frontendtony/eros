namespace Eros.Api.Dto.ApiResponseModels;

public class PaginatedResponseModel<T> : BaseResponseModel
{
    public required List<T> Data { get; set; }
    public required int PageSize { get; set; }
    public required int PageNumber { get; set; }
    public required int Count { get; set; }
    public required int TotalPages { get; set; }
}
