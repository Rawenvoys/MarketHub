using CurrencyRates.Microservices.Rates.Application;
using CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var app = App.Build(args);

using var scope = app.Services.CreateScope();
var ratesDbContext = scope.ServiceProvider.GetRequiredService<RatesDbContext>();
ratesDbContext.Database.Migrate();

//ToDo: Move maps later...
app.MapGet("/", GetCurrentRatesQuery())
   .WithName("Get current rates")
   .WithOpenApi();

app.Run();

static Func<ILogger<Program>, Task> GetCurrentRatesQuery()
{
    return async ([FromServices] ILogger<Program> _logger)
        => _logger.LogDebug("`/`");
}