using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ServiceRequestApp.Blazor;
using ServiceRequestApp.Blazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient {
 BaseAddress = new Uri("http://localhost:5000/")
});

builder.Services.AddScoped<ServiceRequestApi>();

await builder.Build().RunAsync();

