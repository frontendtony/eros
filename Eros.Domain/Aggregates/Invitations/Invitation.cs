namespace Eros.Domain.Aggregates.Invitations;

public class Invitation
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Code { get; init; }
    public required Guid EstateId { get; init; }
    public required Guid RoleId { get; init; }
    public required Guid CreatedBy { get; init; }
    public DateTime Expiration { get; init; } = DateTime.UtcNow.AddDays(7);
    public InvitationStatus Status { get; private set; } = InvitationStatus.Pending;
    
    public bool IsExpired => DateTime.UtcNow > Expiration;
    
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    
    public void Accept()
    {
        if (IsExpired)
        {
            throw new InvalidOperationException("Invitation is expired");
        }
        
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
        if (Status != InvitationStatus.Pending)
        {
            throw new InvalidOperationException("Invitation is not pending");
        }
        
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}