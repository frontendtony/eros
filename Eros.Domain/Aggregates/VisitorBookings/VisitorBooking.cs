using Eros.Domain.Enums;

namespace Eros.Domain.Aggregates.VisitorBookings;

public class VisitorBooking
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public int Code { get; init; } = new Random().Next(100000, 999999);
    public required Guid EstateId { get; init; }
    public required Guid CreatedBy { get; init; }
    public required string Name { get; init; }
    public string? PhoneNumber { get; init; }
    public required Gender Gender { get; init; }
    public string? Purpose { get; init; }
    public VisitorBookingStatus Status { get; private set; } = VisitorBookingStatus.Pending;
    public bool IsExpired => DateTime.Now > ExpiresAt;
    public bool IsDeleted { get; private set; }
    public string? RejectionReason { get; private set; }
    public DateTime CreatedAt { get; init; } = DateTime.Now;
    public required DateTime ExpiresAt { get; init; } = DateTime.Now.Add(TimeSpan.FromHours(1));
    public DateTime? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public void Admit(Guid admittedBy)
    {
        UpdateStatus(VisitorBookingStatus.Admitted, admittedBy);
    }

    public void Reject(Guid rejectedBy, string rejectionReason)
    {
        RejectionReason = rejectionReason;
        UpdateStatus(VisitorBookingStatus.Rejected, rejectedBy);
    }

    public void Delete(Guid deletedBy)
    {
        if (IsDeleted)
        {
            throw new InvalidOperationException("Visitor booking has already been deleted");
        }

        if (Status is VisitorBookingStatus.Admitted or VisitorBookingStatus.Rejected)
        {
            throw new InvalidOperationException("You cannot delete an admitted or rejected visitor booking");
        }

        IsDeleted = true;
        UpdatedBy = deletedBy;
        UpdatedAt = DateTime.Now;
    }

    private void UpdateStatus(VisitorBookingStatus status, Guid updatedBy)
    {
        if (Status is VisitorBookingStatus.Admitted or VisitorBookingStatus.Rejected)
        {
            throw new InvalidOperationException("Visitor has already been admitted or rejected");
        }

        if (IsExpired)
        {
            throw new InvalidOperationException("Visitor booking has expired");
        }

        if (IsDeleted)
        {
            throw new InvalidOperationException("Visitor booking has been deleted");
        }

        Status = status;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
    }
}
