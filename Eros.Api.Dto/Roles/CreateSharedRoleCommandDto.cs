namespace Eros.Api.Dto.Roles;

public record CreateSharedRoleCommandDto(
    string Id,
    string Name,
    string Description
);