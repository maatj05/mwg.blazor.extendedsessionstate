namespace maatwerkgiethoorn.blazor.extendedsessionstate;

public interface ISessionManager<T>  
{
    Task<T> GetSessionAsync();
    Task UpdateSessionAsync(T session);
    
}
