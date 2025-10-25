using MarketHub.Clients.Rates.Client.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddBlazorBootstrap();
builder.Services.AddRatesApiClient(builder.Configuration);

await builder.Build().RunAsync();
