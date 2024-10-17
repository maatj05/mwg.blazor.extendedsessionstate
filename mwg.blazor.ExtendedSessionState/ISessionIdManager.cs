namespace maatwerkgiethoorn.blazor.extendedsessionstate;

 
public interface ISessionIdManager
{
    Task<Guid> GetSessionId();
}
