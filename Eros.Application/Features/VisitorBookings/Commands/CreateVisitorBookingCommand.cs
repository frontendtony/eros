using Eros.Api.Dto.VisitorBookings;
using Eros.Application.Abstractions;
using Eros.Domain.Enums;

namespace Eros.Application.Features.VisitorBookings.Commands;

public record CreateVisitorBookingCommand : ICommand<CreateVisitorBookingCommandDto>
{
    public Guid EstateId { get; set; }
    public Guid CreatedBy { get; set; }
    public required string Name { get; init; }
    public Gender Gender { get; init; }
    public string? Purpose { get; init; }
    public string? PhoneNumber { get; init; }
}
