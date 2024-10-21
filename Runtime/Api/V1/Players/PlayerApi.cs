using PlayerMake.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerApi : BaseApi
{
    public const string Resource = "auth";

    private readonly string _baseUrl;

    public PlayerApi(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    public virtual async Task<PlayerLoginResponse> LoginAync(PlayerLoginRequest request)
    {
        return await PostAsync<PlayerLoginResponse, PlayerLoginRequest>(new RequestWithBody<PlayerLoginRequest>()
        {
            Url = $"{_baseUrl}/v1/{Resource}/login-with-code",
            Payload = request,
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        });
    }
}
