namespace mwg.blazor.extendedsessionstate;

public interface ISessionManager<T> where T : new()
{
    Task<T> GetSessionAsync();
    Task UpdateSessionAsync(T session);
}
