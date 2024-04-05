namespace Eros.Api.Dto.Estates;

public record CreateEstateCommandDto(
    string Id,
    string Name,
    string Address
);