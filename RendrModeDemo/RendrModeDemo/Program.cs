using RendrModeDemo.Client.Pages;
using RendrModeDemo.Components;
using Marimer.Blazor.RenderMode;
using RendrModeDemo.Client;
using RendrModeDemo;
using mwg.blazor.extendedsessionstate;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddHttpContextAccessor()
    .AddExtendedSessionStateServer<AppState>()
    .AddRenderModeDetection()
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();



//voor de state:
builder.Services.AddControllers();








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

//app.MapControllers();

app.UseExtendedSessionState<AppState>();

app    
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(RendrModeDemo.Client._Imports).Assembly);

app.Run();
