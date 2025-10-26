using MarketHub.Microservices.Rates.Infrastructure.Persistance.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Configuration.AddUserSecrets<Program>();
builder.Services.AddPersistance(builder.Configuration);
var app = builder.Build();