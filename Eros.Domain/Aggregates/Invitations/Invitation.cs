namespace Eros.Domain.Aggregates.Invitations;

public class Invitation
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string Email { get; init; }
    public required Guid EstateId { get; init; }
    public required Guid RoleId { get; init; }
    public required Guid CreatedBy { get; init; }
    public Guid? UserId { get; set; }
    public DateTime Expiration { get; init; } = DateTime.UtcNow.AddDays(7);
    public InvitationStatus Status { get; private set; } = InvitationStatus.Pending;

    // this exists to make the invitation code anything other than the Guid, but unique
    public string Code => Id.ToString().GetHashCode().ToString("X");
    public bool IsExistingUser => UserId.HasValue;
    public bool IsExpired => DateTime.UtcNow > Expiration;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }

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

    private void UpdateStatus(InvitationStatus status)
    {
        if (IsExpired)
        {
            throw new InvalidOperationException("Invitation is expired");
        }

        if (Status != InvitationStatus.Pending)
        {
            throw new InvalidOperationException("Invitation is not pending");
        }

        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}