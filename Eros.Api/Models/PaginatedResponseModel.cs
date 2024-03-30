namespace Eros.Api.Models;

public class PaginatedResponseModel<T> : BaseResponseModel
{
    public required List<T> Data { get; set; }
    public required int PageSize { get; set; }
    public required int PageNumber { get; set; }
    public required int Total { get; set; }
}