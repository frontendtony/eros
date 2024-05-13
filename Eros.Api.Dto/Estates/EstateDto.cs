namespace Eros.Api.Dto.Estates;

public record EstateDto
{
  public string Id { get; init; }
  public string Name { get; init; }
  public string Address { get; init; }
}
