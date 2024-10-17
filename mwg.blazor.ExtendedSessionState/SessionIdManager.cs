

using Microsoft.AspNetCore.Http;

namespace mwg.blazor.extendedsessionstate;

public class SessionIdManager(IHttpContextAccessor httpContextAccessor) : ISessionIdManager
{
    private readonly IHttpContextAccessor HttpContextAccessor = httpContextAccessor;

    public Task<Guid> GetSessionId()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        Guid? result;

        if (httpContext != null)
        {
            if (httpContext.Request.Cookies.ContainsKey("sessionId"))
            {
                result = new Guid(httpContext.Request.Cookies["sessionId"]!);
            }
            else
            {
                result = Guid.NewGuid();
                httpContext.Response.Cookies.Append("sessionId", result.ToString()!);
            }
            return Task.FromResult(result.Value);
        }

        throw new InvalidOperationException("No HttpContext available");



    }
}
