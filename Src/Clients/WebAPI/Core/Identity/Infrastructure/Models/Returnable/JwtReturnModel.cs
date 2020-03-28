using Newtonsoft.Json;

namespace Shop.Clients.WebApi.Core.Identity.Infrastructure.Models.Returnable
{
    public class JwtReturnModel
    {
        [JsonProperty("access_token")] public string AccessToken { get; set; }

        [JsonProperty("token_type")] public string TokenType { get; set; }

        [JsonProperty("expires_in")] public long ExpiresIn { get; set; }
    }
}