using Client;
using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// -- CLIENT-PROGRAM.CS 

var builder = WebAssemblyHostBuilder.CreateDefault(args);
// Add Component
// builder.Services.AddSingleton<InMemoryDatabaseCache, InMemoryDatabaseCache > ();
//
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Replaced for singleton
// builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// SINGLETON [Make available everywhere]
builder.Services.AddSingleton<HttpClient>();
// -- 
builder.Services.AddSingleton<InMemoryDatabaseCache>();



await builder.Build().RunAsync();
