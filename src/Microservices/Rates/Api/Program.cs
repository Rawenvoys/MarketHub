using MarketHub.Microservices.Rates.Application;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Contexts;
using MarketHub.Microservices.Rates.Infrastructure.Persistance.Seeds;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RB.SharedKernel.MediatR.Extensions;
using GetLastTableQuery = MarketHub.Microservices.Rates.Application.Queries.GetLastTable.Query;
using GetLatestRatesResult = MarketHub.Microservices.Rates.Application.Queries.GetLastTable.Result;
using GetMetaQuery = MarketHub.Microservices.Rates.Application.Queries.GetMeta.Query;
using GetMetaResult = MarketHub.Microservices.Rates.Application.Queries.GetMeta.Result;



var app = App.Build(args);

using var scope = app.Services.CreateScope();
var ratesDbContext = scope.ServiceProvider.GetRequiredService<RatesDbContext>();
ratesDbContext.Database.Migrate();
var syncStateSeeder = scope.ServiceProvider.GetRequiredService<SyncStateSeeder>();
await syncStateSeeder.SeedAsync();


app.UseRouting();

//ToDo: Move maps later...
app.MapGet("/tables/last", async ([FromServices] IMediator _mediator, [FromServices] ILogger<Program> _logger) =>
{
    var result = await _mediator.SendQueryAsync(new GetLastTableQuery());
    if (result == null || result.Table == null)
        return Results.NotFound();
    return Results.Ok(result.Table);
})
   .Produces<GetLatestRatesResult>(StatusCodes.Status200OK, "application/json")
   .Produces(StatusCodes.Status404NotFound)
   .WithTags("Table")
   .WithOpenApi();


app.MapGet("/meta", async ([FromServices] IMediator _mediator, [FromServices] ILogger<Program> _logger) =>
{
    var result = await _mediator.SendQueryAsync(new GetMetaQuery());
    if (result == null || result.Meta == null)
        return Results.NotFound();
    return Results.Ok(result.Meta);
})
.Produces<GetMetaResult>(StatusCodes.Status200OK, "application/json")
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