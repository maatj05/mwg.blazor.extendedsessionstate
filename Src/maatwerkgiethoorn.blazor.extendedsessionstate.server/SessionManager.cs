using Microsoft.AspNetCore.Http;

namespace maatwerkgiethoorn.blazor.extendedsessionstate.server;

public class SessionManager<T> : ISessionManager<T> where T : new()
{

    private readonly ISessionIdManager _sessionIdManager;
    private readonly ISessionRepository<T> _sessionRepository;

    public SessionManager(ISessionIdManager sessionIdManager, ISessionRepository<T> sessionRepository)
    {
        _sessionIdManager = sessionIdManager;
        _sessionRepository = sessionRepository;
    }

    public async Task<T> GetSessionAsync()
    {
        var key = await _sessionIdManager.GetSessionId();

        var session = await _sessionRepository.Get(key);

        var endTime = DateTime.Now + TimeSpan.FromSeconds(10);
        //while (session.IsCheckedOut)
        //{
        //    if (DateTime.Now > endTime)
        //        throw new TimeoutException();
        //    await Task.Delay(5);
        //}

        return session;
    }

    public async Task UpdateSessionAsync(T session)
    {
        if (session != null)
        {
            var key = await _sessionIdManager.GetSessionId();
            await _sessionRepository.Update(key, session);
        }
    }
}
