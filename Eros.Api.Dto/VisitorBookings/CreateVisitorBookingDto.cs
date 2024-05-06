namespace Eros.Api.Dto.VisitorBookings;

public record CreateVisitorBookingDto
{
    public required string Name { get; set; }
    public required string Gender { get; set; }
    public string? Purpose { get; set; }
    public string? PhoneNumber { get; set; }
}
