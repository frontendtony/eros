namespace Eros.Api.Dto.Roles;

public record CreateSharedRoleCommandDto(
    string Name,
    string Description
);