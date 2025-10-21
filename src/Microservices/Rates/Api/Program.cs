using CurrencyRates.Microservices.Rates.Application;
using Microsoft.AspNetCore.Mvc;

var app = App.Build(args);

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