using Microsoft.Extensions.DependencyInjection;

namespace mwg.blazor.extendedsessionstate;

public static class WebApplicationExtensions
{
    //public static WebApplication UseExtendedSessionState(this WebApplication app)
    //{
    //    app.UseEndpoints(endpoints =>
    //    {
    //        endpoints.MapGet("/State", async (ISessionManager sessionManager) =>
    //        {
    //            var session = await sessionManager.GetSessionAsync();
    //            return Results.Json(session);
    //        });

    //        endpoints.MapPut("/State", async (ISessionManager sessionManager, AppState updatedSession) =>
    //        {
    //            await sessionManager.UpdateSessionAsync(updatedSession);
    //            return Results.Ok();
    //        });
    //    });

    //    return app;
    //}






    public static IServiceCollection AddExtendedSessionStateServer<T>(this IServiceCollection services) where T : class, new()
    {
        // Add HttpContextAccessor
        //services.AddHttpContextAccessor();

        // Register ISessionManager as a Singleton

        services.AddSingleton<ISessionManager<T>, SessionManager<T>>();

        // Register ISessionIdManager as a Transient service
        services.AddTransient<ISessionIdManager, SessionIdManager>();

        return services;




    }
    public static IServiceCollection AddExtendedSessionStateWebAssembly<T>(this IServiceCollection services,Uri hostAddress) where T : class, new()
    {

        

        services.AddTransient<HttpClient>(sp => new HttpClient { BaseAddress = hostAddress });
        services.AddSingleton<ISessionManager<T>, SessionManagerWebAsembly<T>>();
        return services;
    }
}
