using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Marimer.Blazor.RenderMode.WebAssembly;

using mwg.blazor.extendedsessionstate;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddExtendedSessionStateWebAssembly<AppState>(new Uri(builder.HostEnvironment.BaseAddress));

await builder.Build().RunAsync();

builder.Services.AddRenderModeDetection();