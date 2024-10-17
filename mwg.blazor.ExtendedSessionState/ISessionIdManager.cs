namespace mwg.blazor.extendedsessionstate;

 
public interface ISessionIdManager
{
    Task<Guid> GetSessionId();
}
