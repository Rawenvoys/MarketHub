using MarketHub.Clients.Nbp.Client.Extensions;
using MarketHub.Clients.Rates.Client;
using MarketHub.Clients.Rates.Client.Extensions;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddBlazorBootstrap();

builder.Services.AddNbpApiClient(builder.Configuration);

// builder.Services.AddRefitClient<IRatesApi>()
//             .ConfigureHttpClient((serviceProvider, httpClient) =>
//             {
//                 var baseUri = builder.Configuration.GetSection("RatesApi:BaseUri").Value
//                     ?? throw new InvalidOperationException($"");


//                 httpClient.BaseAddress = new Uri(baseUri);
//             });


await builder.Build().RunAsync();
