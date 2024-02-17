namespace Eros.Domain.Aggregates.Roles;

public interface IRoleReadRepository
{
    Task<Role?> GetByIdAsync(Guid id);
    Task<IEnumerable<Role>> GetAllAsync();
}
