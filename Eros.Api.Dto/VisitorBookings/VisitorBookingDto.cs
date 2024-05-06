namespace Eros.Api.Dto.VisitorBookings;

public record VisitorBookingDto(
    string Id,
    string EstateId,
    string CreatedBy,
    string Name,
    string? Purpose,
    string PhoneNumber,
    string Status,
    string RejectionReason,
    string Gender,
    bool IsDeleted,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    DateTime? ExpiresAt,
    string UpdatedBy
);
