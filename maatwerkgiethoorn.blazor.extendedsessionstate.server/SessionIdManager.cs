

using Microsoft.AspNetCore.Http;

namespace maatwerkgiethoorn.blazor.extendedsessionstate;

public class SessionIdManager(IHttpContextAccessor httpContextAccessor) : ISessionIdManager
{
    readonly string _sessionName = "__extendedSession";
    private readonly IHttpContextAccessor HttpContextAccessor = httpContextAccessor;

    public Task<Guid> GetSessionId()
    {
        var httpContext = HttpContextAccessor.HttpContext;
        Guid? result;

        if (httpContext != null)
        {
            if (httpContext.Request.Cookies.ContainsKey(_sessionName))
            {
                result = new Guid(httpContext.Request.Cookies[_sessionName]!);
            }

            else if (httpContext.Session.Keys.Contains(_sessionName))
            {
                result = new Guid(httpContext.Session.GetString(_sessionName)!);
            }

            else
            {
                result = Guid.NewGuid();
            }
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(1),
                HttpOnly = true,
                Secure = true
            };

            httpContext.Response.Cookies.Append(_sessionName, result.ToString()!, cookieOptions);
            httpContext.Session.SetString(_sessionName, result.ToString()!);


            return Task.FromResult(result.Value);
        }

        throw new InvalidOperationException("No HttpContext available");



    }
}
