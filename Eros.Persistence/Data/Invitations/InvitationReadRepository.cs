using Eros.Domain.Aggregates.Invitations;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Invitations;

public class InvitationReadRepository(
    ErosDbContext _dbContext
) : IInvitationReadRepository
{
    public async Task<Invitation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Invitations
            .Where(i => i.Id == id && DateTime.UtcNow < i.Expiration)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Invitation>> GetByEstateIdAsync(Guid estateId, CancellationToken cancellationToken)
    {
        return await _dbContext.Invitations
            .Where(i => i.EstateId == estateId && DateTime.UtcNow < i.Expiration)
            .ToListAsync(cancellationToken);
    }

    public async Task<Invitation?> GetByEstateIdAndUserIdAsync(Guid estateId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Invitations
            .Where(i => i.EstateId == estateId && i.UserId == userId && DateTime.UtcNow < i.Expiration)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Invitation?> GetByEstateIdAndEmailAsync(Guid estateId, string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Invitations
            .Where(i => i.EstateId == estateId && i.Email == email && DateTime.UtcNow < i.Expiration)
            .FirstOrDefaultAsync(cancellationToken);
    }
}