using CurrencyRates.Clients.Nbp.Client;
using CurrencyRates.Clients.Nbp.Client.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddNbpApiClient(builder.Configuration);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", async ([FromServices] INbpApi _nbpApi, [FromServices] ILogger<Program> _logger) =>
{
    _logger.LogDebug("`../`");
})
.WithName("Get current rates")
.WithOpenApi();

app.Run();

