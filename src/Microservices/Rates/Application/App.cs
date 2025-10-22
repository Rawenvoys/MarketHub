using CurrencyRates.Microservices.Rates.Application.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace CurrencyRates.Microservices.Rates.Application;

public static class App
{
    public static WebApplication Build(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Logging.AddApplication();
        builder.Services.AddApplication(builder.Configuration);
        return builder.Build();
    }

    public static void UseApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
    }
}
