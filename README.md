# MWG.Blazor.ExtendedSessionState

'MWG.Blazor.ExtendedSessionState'  is a package that extends the session manager for a .NET 8 Blazor Web application. It can be used to ensure the app state is synchronized between the server and WebAssembly with automatic re-rendering whenever the state changes.

## Installation

Install the package via NuGet:

```bash
dotnet add package MWG.Blazor.ExtendedSessionState
```
## Usage
### Server-side setup
To use this package in a Blazor Server project, follow these steps:

1. Open your Program.cs file.
2. Add the following lines to register the necessary services and middleware:

```csharp
using mwg.blazor.extendedsessionstate;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpContextAccessor()  // Ensure HttpContextAccessor is available
    .AddExtendedSessionStateServer<AppState>();  // Register extended session state service

var app = builder.Build();

app.UseExtendedSessionState<AppState>();  // Activate extended session state
```
- Replace AppState with any serializable class that has a default constructor and represents the state of your application.

### WebAssembly setup
For Blazor WebAssembly projects, follow these steps:

1. Open your Program.cs file.
2. Add the following lines to register the session state services:
``` csharp
using mwg.blazor.extendedsessionstate;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services
    .AddExtendedSessionStateWebAssembly<AppState>(new Uri(builder.HostEnvironment.BaseAddress));  // Register extended session state for WebAssembly

await builder.Build().RunAsync();
```
### AppState Example
The AppState class can be any serializable class or record. Here's an example of a simple AppState class:

``` csharp
[Serializable]
public class AppState
{
    public int CounterCount { get; set; }
}
```
This class holds the shared state of your application. You can extend it with other properties as needed to maintain the state across the client and server.

### Blazor component example
You can use the state in your componentas like this:

``` csharp
@page "/counter"
@rendermode InteractiveAuto
@inject ISessionManager<AppState> SessionManager
<PageTitle>Counter</PageTitle>
<h1>Counter</h1>

<p role="status">Current count: @currentCount</p>
<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
 AppState? session;
    protected override async Task OnInitializedAsync()
    {
        session = await SessionManager.GetSessionAsync();
        currentCount = session.CounterCount;
    }

    private async Task IncrementCount()
    {
        currentCount++;
        session.CurrentCount = currentCount;
        
        await SessionManager.UpdateSessionAsync(session);
    }
}

```

