using DynamicSun.Client;
using DynamicSun.Integration.Client.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var client_uri = builder.Configuration.GetValue<string>("Integrations:Admin:Address");

builder.Services.AddRefitClient<IDynamicSunClient>()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(client_uri));

builder.Services.AddMudServices();

await builder.Build().RunAsync();