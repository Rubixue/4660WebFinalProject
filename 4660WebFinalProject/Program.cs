using _4660WebFinalProject;
using _4660FinalProject.Services; // Add reference to your services
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient as a scoped service for API requests
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register your custom services
builder.Services.AddScoped<TemporalCoalescingService>();
builder.Services.AddScoped<JSONToMySQLService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7235") });
builder.Services.AddMudServices();



await builder.Build().RunAsync();
