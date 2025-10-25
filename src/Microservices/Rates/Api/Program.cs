using MarketHub.Microservices.Rates.Application;
using MarketHub.Microservices.Rates.Domain.Aggregates;
using MarketHub.Microservices.Rates.Domain.Interfaces.Repositories;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Contexts;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Seeds;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RB.SharedKernel.MediatR.Extensions;
using GetLatestRatesQuery = MarketHub.Microservices.Rates.Application.Queries.GetLatestRates.Query;
using GetLatestRatesResult = MarketHub.Microservices.Rates.Application.Queries.GetLatestRates.Result;

var app = App.Build(args);

using var scope = app.Services.CreateScope();
var ratesDbContext = scope.ServiceProvider.GetRequiredService<RatesDbContext>();
ratesDbContext.Database.Migrate();
var syncStateSeeder = scope.ServiceProvider.GetRequiredService<SyncStateSeeder>();
await syncStateSeeder.SeedAsync();

//ToDo: Move maps later...
app.MapGet("/", async ([FromServices] IMediator _mediator, [FromServices] ILogger<Program> _logger) => Results.Ok(await _mediator.SendQueryAsync(new GetLatestRatesQuery())))
   .WithName("Get latest currency rates")
   .WithTags("CurrencyRates")
   .WithOpenApi();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = []
});

app.UseHttpsRedirection();
app.Run();