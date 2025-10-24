using CurrencyRates.Microservices.Rates.Application;
using CurrencyRates.Microservices.Rates.Application.Interfaces;
using CurrencyRates.Microservices.Rates.Application.Services;
using CurrencyRates.Microservices.Rates.Domain.Aggregates;
using CurrencyRates.Microservices.Rates.Domain.Interfaces.Repositories;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Seeds;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var app = App.Build(args);

using var scope = app.Services.CreateScope();
var ratesDbContext = scope.ServiceProvider.GetRequiredService<RatesDbContext>();
ratesDbContext.Database.Migrate();
var syncStateSeeder = scope.ServiceProvider.GetRequiredService<SyncStateSeeder>();
await syncStateSeeder.SeedAsync();

//ToDo: Move maps later...
app.MapGet("/sources", GetActiveSourcesQuery())
   .WithName("Get active sources")
   .WithTags("Source")
   .WithOpenApi();


app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = []
});


app.UseHttpsRedirection();
app.Run();

static Func<ISourceRepository, ILogger<Program>, Task<IEnumerable<Source>>> GetActiveSourcesQuery()
    => async ([FromServices] _sourceRepository, [FromServices] _logger)
        =>
        {
            _logger.LogInformation("Get active sources `/sources`");
            return await _sourceRepository.GetActiveAsync();
        };
