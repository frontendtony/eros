namespace Eros.Domain.Aggregates.Roles;

public interface IRoleReadRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<IEnumerable<Role>> GetAllAsync();
    Task<IEnumerable<Role>> GetSharedRolesAsync(CancellationToken cancellationToken);
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken);
    Task<bool> HasPermissionsAsync(Guid roleId, IEnumerable<Guid> permissions, CancellationToken cancellationToken);
}
