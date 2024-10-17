using System.Net.Http.Json;

namespace maatwerkgiethoorn.blazor.extendedsessionstate;

public class SessionManagerWebAsembly<T>(HttpClient client) : ISessionManager<T> where T : class, new()
{
    private T? _session;

    public async Task<T> GetSessionAsync()
    {
        _session = await client.GetFromJsonAsync<T>("state");
        if (_session == null)
            throw new InvalidOperationException("Session not found");
        return _session;
    }

    public async Task UpdateSessionAsync(T session)
    {
        await client.PutAsJsonAsync<T>("state", session);
        _session = session;
    }
}