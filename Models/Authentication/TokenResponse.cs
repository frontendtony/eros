namespace EstateManager.Models;

public class TokenResponse
{
    public string Token { get; set; } = String.Empty;
    public DateTime Expiration { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
