namespace Eros.Domain.Aggregates.Invitations;

public class Invitation
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Email { get; init; }
    public required Guid EstateId { get; init; }
    public required Guid RoleId { get; init; }
    public required Guid CreatedBy { get; init; }
    public Guid? UserId { get; private set; }
    public string Code { get; init; }
    public DateTime Expiration { get; private set; } = DateTime.UtcNow.AddDays(7);
    public InvitationStatus Status { get; private set; } = InvitationStatus.Pending;

    public bool IsExistingUser => UserId.HasValue;
    public bool IsExpired => DateTime.UtcNow > Expiration;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

    public void MapUser(Guid userId)
    {
        if (IsExistingUser) throw new InvalidOperationException("Invitation already has a user id");

        UserId = userId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Accept()
    {
        UpdateStatus(InvitationStatus.Accepted);
    }

    public void Reject()
    {
        UpdateStatus(InvitationStatus.Rejected);
    }

    public void Cancel()
    {
        UpdateStatus(InvitationStatus.Cancelled);
    }

    public void ResetExpiration()
    {
        Expiration = DateTime.UtcNow.AddDays(7);
        UpdatedAt = DateTime.UtcNow;
    }

    private void UpdateStatus(InvitationStatus status)
    {
        if (IsExpired) throw new InvalidOperationException("Invitation is expired");

        if (Status != InvitationStatus.Pending) throw new InvalidOperationException("Invitation is not pending");

        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}
