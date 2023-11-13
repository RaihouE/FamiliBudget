using System.Text.Json.Serialization;

namespace FamiliBudget.App.Infrastructure.Auth;

public class TokenResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = null!;
    [JsonPropertyName("expiration")]
    public DateTimeOffset Expiration { get; set; }
}
