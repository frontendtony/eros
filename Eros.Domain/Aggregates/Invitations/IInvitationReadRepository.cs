namespace Eros.Domain.Aggregates.Invitations;

public interface IInvitationReadRepository
{
    public Task<Invitation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<Invitation>> GetByEstateIdAsync(Guid estateId, CancellationToken cancellationToken);
    public Task<Invitation?> GetByEstateIdAndUserIdAsync(Guid estateId, Guid userId, CancellationToken cancellationToken);
    public Task<Invitation?> GetByEstateIdAndEmailAsync(Guid estateId, string email, CancellationToken cancellationToken);
}