namespace Eros.Domain.Aggregates.Roles;

public interface IPermissionReadRepository
{
    Task<IEnumerable<Permission>> GetAllAsync();
}
