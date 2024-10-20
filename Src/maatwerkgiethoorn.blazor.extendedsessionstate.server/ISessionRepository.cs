
namespace maatwerkgiethoorn.blazor.extendedsessionstate;

public interface ISessionRepository<T>  
{
    Task<T?> Get(Guid key);
    Task Update(Guid key, T session);
}
