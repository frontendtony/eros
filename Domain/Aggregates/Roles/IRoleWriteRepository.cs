namespace Eros.Domain.Aggregates.Roles;

public interface IRoleWriteRepository
{
    Task<Role?> AddAsync(Role role);
    Task<Role?> UpdateAsync(Role role);
    Task<bool> DeleteAsync(Guid id);
}
