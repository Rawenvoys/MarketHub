using MarketHub.Microservices.Rates.Application;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Contexts;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Seeds;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RB.SharedKernel.MediatR.Extensions;
using GetLatestRatesQuery = MarketHub.Microservices.Rates.Application.Queries.GetLatestRates.Query;
using GetLatestRatesResult = MarketHub.Microservices.Rates.Application.Queries.GetLatestRates.Result;

var app = App.Build(args);

using var scope = app.Services.CreateScope();
var ratesDbContext = scope.ServiceProvider.GetRequiredService<RatesDbContext>();
ratesDbContext.Database.Migrate();
var syncStateSeeder = scope.ServiceProvider.GetRequiredService<SyncStateSeeder>();
await syncStateSeeder.SeedAsync();


app.UseRouting();

//ToDo: Move maps later...
app.MapGet("/", async ([FromServices] IMediator _mediator, [FromServices] ILogger<Program> _logger) =>
{
    var result = await _mediator.SendQueryAsync(new GetLatestRatesQuery());
    if (result == null || result.CurrencyRateTable == null)
        return Results.NotFound();
    return Results.Ok(result.CurrencyRateTable);
})
   .Produces<GetLatestRatesResult>(StatusCodes.Status200OK, "application/json")
   .Produces(StatusCodes.Status404NotFound)
   .WithTags("CurrencyRates")
   .WithOpenApi();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = []
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rates API");
});

app.UseHttpsRedirection();
app.Run();