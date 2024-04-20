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
            .Where(i => i.Id == id && !i.IsExpired)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<Invitation>> GetByEstateIdAsync(Guid estateId, CancellationToken cancellationToken)
    {
        return await _dbContext.Invitations
            .Where(i => i.EstateId == estateId && !i.IsExpired)
            .ToListAsync(cancellationToken);
    }

    public async Task<Invitation?> GetByEstateIdAndUserIdAsync(Guid estateId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Invitations
            .Where(i => i.EstateId == estateId && i.UserId == userId && !i.IsExpired)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Invitation?> GetByEstateIdAndEmailAsync(Guid estateId, string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Invitations
            .Where(i => i.EstateId == estateId && i.Email == email && !i.IsExpired)
            .FirstOrDefaultAsync(cancellationToken);
    }
}