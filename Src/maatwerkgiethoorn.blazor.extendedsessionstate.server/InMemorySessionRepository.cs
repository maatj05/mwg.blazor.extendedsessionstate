namespace maatwerkgiethoorn.blazor.extendedsessionstate.server;

public class InMemorySessionRepository<T> : ISessionRepository<T>  
{

    private readonly Dictionary<Guid, T> _sessionList = [];
    public async Task<T?> Get(Guid key)
    {
        T session;

        if (!_sessionList.ContainsKey(key))
        {
            session = default!;
            _sessionList.Add(key, session);
        }
        session = _sessionList[key];
        return await Task.FromResult(session);
    }

    public async Task Update(Guid key, T session)
    {
        _sessionList[key] = session;
        await Task.CompletedTask;
    }
}
