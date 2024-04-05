namespace Eros.Domain.Aggregates.Roles;

public interface IRoleWriteRepository
{
    Task<Role?> AddAsync(Role role, CancellationToken cancellationToken = default);
    Task<Role?> UpdateAsync(Role role, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
