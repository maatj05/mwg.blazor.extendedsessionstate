
namespace mwg.blazor.extendedsessionstate
{
    public interface ISessionRepository<T> where T : new()
    {
        Task<T> Get(Guid key);
        Task Update(Guid key, T session);
    }
}