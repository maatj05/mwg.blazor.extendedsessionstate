using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace maatwerkgiethoorn.blazor.extendedsessionstate.server;

public static class WebApplicationExtensions
{
    public static Microsoft.AspNetCore.Builder.WebApplication UseExtendedSessionState<T>(this WebApplication app) where T : new()
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


    public static IServiceCollection AddExtendedSessionStateServer<T>(this IServiceCollection services) where T : class, new()
    {
        // Add HttpContextAccessor

        //only add if not already added
        if (!services.Any(x => x.ServiceType == typeof(IHttpContextAccessor)))
        {
            services.AddHttpContextAccessor();
        }
        

        // Register ISessionManager as a Singleton
        services.AddSingleton<ISessionManager<T>, SessionManager<T>>();

        // Register ISessionIdManager as a Transient service
        services.AddTransient<ISessionIdManager, SessionIdManager>();

        // Check if a custom ISessionRepository has been registered; if not, register the default one
        if (!services.Any(x => x.ServiceType == typeof(ISessionRepository<T>)))
        {
            services.AddSingleton<ISessionRepository<T>, InMemorySessionRepository<T>>();
        }

        return services;
    }

    //public static IServiceCollection AddExtendedSessionStateServer<T>(this IServiceCollection services, Func<IServiceProvider, ISessionRepository<T>> repositoryFactory) where T : class, new()
    //{
    //    // Add HttpContextAccessor
    //    //services.AddHttpContextAccessor();

    //    // Register ISessionManager as a Singleton
    //    services.AddSingleton<ISessionManager<T>, SessionManager<T>>();

    //    // Register ISessionIdManager as a Transient service
    //    services.AddTransient<ISessionIdManager, SessionIdManager>();

    //    // Use custom repository implementation
    //    services.AddSingleton<ISessionRepository<T>>(repositoryFactory);

    //    return services;
    //}



    //public static IServiceCollection AddExtendedSessionStateWebAssembly<T>(this IServiceCollection services,Uri hostAddress) where T : class, new()
    //{
    //    services.AddTransient<HttpClient>(sp => new HttpClient { BaseAddress = hostAddress });
    //    services.AddSingleton<ISessionManager<T>, SessionManagerWebAsembly<T>>();
    //    return services;
    //}
}
