using EstateManager.Constants;

public record PaginationQueryModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = GlobalConstants.DefaultPageSize;
}
