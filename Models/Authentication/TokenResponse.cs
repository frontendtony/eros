namespace EstateManager.Models;

public class TokenResponse
{
    public string Token { get; set; } = String.Empty;
    public DateTime Expiration { get; set; }
}
