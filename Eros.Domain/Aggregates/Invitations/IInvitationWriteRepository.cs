namespace Eros.Domain.Aggregates.Invitations;

public interface IInvitationWriteRepository
{
    Task AddAsync(Invitation invitation, CancellationToken cancellationToken = default);
    void UpdateAsync(Invitation invitation, CancellationToken cancellationToken = default);
}