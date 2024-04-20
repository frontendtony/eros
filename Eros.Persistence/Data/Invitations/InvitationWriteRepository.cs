using Eros.Domain.Aggregates.Invitations;

namespace Eros.Persistence.Data.Invitations;

public class InvitationWriteRepository(ErosDbContext dbContext) : IInvitationWriteRepository
{
    public async Task AddAsync(Invitation invitation, CancellationToken cancellationToken = default)
    {
        await dbContext.Invitations.AddAsync(invitation, cancellationToken);
    }

    public void UpdateAsync(Invitation invitation, CancellationToken cancellationToken = default)
    {
        dbContext.Invitations.Update(invitation);
    }
}