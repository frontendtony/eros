namespace Eros.Api.Dto.VisitorBookings;

public record CreateVisitorBookingDto
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public string? Purpose { get; set; }
    public string? PhoneNumber { get; set; }
}
