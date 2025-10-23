using System;
using CurrencyRates.Microservices.Rates.Domain.ValueObjects.Source;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CurrencyRates.Microservices.Rates.Infrastructure.Persistance.Converters;

public class CronExpressionConverter() : ValueConverter<Cron, string>(v => v.Expression(), s=> Cron.FromValue(s)) { }
