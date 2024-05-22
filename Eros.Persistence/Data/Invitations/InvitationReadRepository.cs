using Eros.Domain.Aggregates.Invitations;
using Microsoft.EntityFrameworkCore;

namespace Eros.Persistence.Data.Invitations;

public class InvitationReadRepository(
  ErosDbContext dbContext
) : IInvitationReadRepository
{
  public async Task<Invitation?> GetByCodeAsync(string code, CancellationToken cancellationToken)
  {
    return await dbContext.Invitations
      .Where(i => i.Code == code)
      .FirstOrDefaultAsync(cancellationToken);
  }

  public async Task<IEnumerable<Invitation>> GetByEstateIdAsync(Guid estateId, CancellationToken cancellationToken)
  {
    return await dbContext.Invitations
      .Where(i => i.EstateId == estateId)
      .ToListAsync(cancellationToken);
  }

  public async Task<Invitation?> GetByEstateIdAndUserIdAsync(Guid estateId, Guid userId,
    CancellationToken cancellationToken)
  {
    return await dbContext.Invitations
      .Where(i => i.EstateId == estateId && i.UserId == userId)
      .FirstOrDefaultAsync(cancellationToken);
  }

  public async Task<Invitation?> GetByEstateIdAndEmailAsync(Guid estateId, string email,
    CancellationToken cancellationToken)
  {
    return await dbContext.Invitations
      .Where(i => i.EstateId == estateId && i.Email == email && DateTime.UtcNow < i.Expiration)
      .FirstOrDefaultAsync(cancellationToken);
  }
}
