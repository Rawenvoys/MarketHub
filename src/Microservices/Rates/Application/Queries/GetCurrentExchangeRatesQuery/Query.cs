namespace CurrencyRates.Microservices.Rates.Application.Queries.GetCurrentRatesQuery;

public record Query
{
    // Parametry zapytania (zgodne z API GET /rates?...)
    public int PageIndex { get; init; } 
    public int PageSize { get; init; } = 10;
    
    // Sortowanie po kolumnach (np. "Name", "Code", "Value")
    public string SortBy { get; init; } = "Name"; 
    public string SortDirection { get; init; } = "asc";

    // Pojedyncze pole tekstowe do filtrowania po nazwie LUB kodzie
    public string Filter { get; init; } = "";
}