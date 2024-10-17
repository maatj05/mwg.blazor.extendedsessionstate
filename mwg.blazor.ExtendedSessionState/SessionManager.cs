namespace mwg.blazor.extendedsessionstate;

public class SessionManager<T> : ISessionManager<T>  where T: new()
{

    private readonly ISessionIdManager _sessionIdManager;
    private readonly Dictionary<Guid, T> _sessions = [];

    public SessionManager(ISessionIdManager sessionIdManager)
    {
        _sessionIdManager = sessionIdManager;
    }

    public async Task<T> GetSessionAsync()
    {
        var key = await _sessionIdManager.GetSessionId();

        T session;

        if (!_sessions.ContainsKey(key))
        {

            //generate a random name
            var names = new[] { "Alice", "Bob", "Charlie", "David", "Eve", "Frank", "Grace", "Heidi" };
            var random = new Random();
            var name = names[random.Next(names.Length)];

            session = new();
            _sessions.Add(key, session);
        }



        session = _sessions[key];



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
            _sessions[key] = session;
        }
    }
}
