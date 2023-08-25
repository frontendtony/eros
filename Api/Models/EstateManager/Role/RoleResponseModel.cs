using EstateManager.Entities;

namespace Api.ResponseModels;

public class RoleResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<EstatePermission> Permissions { get; set; } = new();
}
