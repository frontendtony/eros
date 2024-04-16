using Eros.Domain.Aggregates.Invitations;

namespace Eros.Persistence.Data;

public class InvitationWriteRepository(ErosDbContext dbContext) : IInvitationWriteRepository
{
    public async Task AddAsync(Invitation invitation, CancellationToken cancellationToken = default)
    {
        await dbContext.Invitations.AddAsync(invitation, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}