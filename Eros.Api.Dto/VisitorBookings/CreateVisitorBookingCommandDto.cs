namespace Eros.Api.Dto.VisitorBookings;

public record CreateVisitorBookingCommandDto
{
    public Guid Id { get; set; }
    public required string Name { get; init; }
    public required string Gender { get; init; }
    public string? Purpose { get; init; }
    public string? PhoneNumber { get; init; }
}
