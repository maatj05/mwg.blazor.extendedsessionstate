using mwg.blazor.ExtendedSessionState;
using RendrModeDemo.Client;

namespace RendrModeDemo;

public static class WebApplicationExtensions
{
    public static WebApplication UseExtendedSessionState<T>(this WebApplication app) where T : new()
    {
        
            app.MapGet("/State", async (ISessionManager<T> sessionManager) =>
            {
                var session = await sessionManager.GetSessionAsync();
                return Results.Json(session);
            });

            app.MapPut("/State", async (ISessionManager<T> sessionManager, T updatedSession) =>
            {
                await sessionManager.UpdateSessionAsync(updatedSession);
                return Results.Ok();
            });
     

        return app;
    }  
}

