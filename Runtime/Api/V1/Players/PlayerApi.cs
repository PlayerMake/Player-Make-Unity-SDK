using PlayerMake.Api;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PlayerApi : BaseApi
{
    public const string Resource = "auth";

    private readonly PlayerMakeSettings _settings;

    public PlayerApi(PlayerMakeSettings settings) : base(settings)
    {
        _settings = settings;
    }

    public virtual async Task<PlayerLoginResponse> LoginAync(PlayerLoginRequest request, RequestCallbacks callbacks)
    {
        return await PostAsync<PlayerLoginResponse, PlayerLoginRequest>(new RequestWithBody<PlayerLoginRequest>()
        {
            Url = $"{_settings.ApiBaseUrl}/v1/{Resource}/login-with-code",
            Payload = request,
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } },
            Callbacks = callbacks
        });
    }
}
