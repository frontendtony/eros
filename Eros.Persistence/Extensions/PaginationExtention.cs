using Eros.Api.Dto.ApiResponseModels;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Extensions;

public static class PaginationExtensions
{
  public static async Task<PaginatedResponseModel<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int pageNumber,
    int pageSize,
    CancellationToken cancellationToken = default)
  {
    pageNumber = pageNumber < 1 ? 1 : pageNumber;
    pageSize = pageSize < 1 ? 10 : pageSize;

    var items = await query.Page(pageNumber, pageSize).ToListAsync(cancellationToken).ConfigureAwait(false);
    var count = await query.CountAsync(cancellationToken).ConfigureAwait(false);
    var totalPages = (int)Math.Ceiling(count / (double)pageSize);

    return new PaginatedResponseModel<T>
    {
      Count = count,
      Data = items,
      PageNumber = pageNumber,
      PageSize = pageSize,
      TotalPages = totalPages
    };
  }

  private static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageLength)
  {
    var page = pageNumber - 1;
    var rowsToSkip = page * pageLength;
    return query.Skip(rowsToSkip).Take(pageLength);
  }
}
