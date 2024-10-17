using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Marimer.Blazor.RenderMode.WebAssembly;

using maatwerkgiethoorn.blazor.extendedsessionstate;
using RendrModeDemo.Client;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddExtendedSessionStateWebAssembly<AppState>(new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddRenderModeDetection();

var app = builder.Build();

await app.RunAsync();
