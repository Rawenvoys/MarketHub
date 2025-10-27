# MarketHub – Opis Projektu

## ⚙️ Podsumowanie Technologiczne

| Warstwa | Technologia |
|----------|--------------|
| **Backend** | .NET 8, Minimal API, EF Core, Hangfire |
| **Frontend** | Blazor Web App (Bootstrap Blazor) |
| **Integracja** | Refit (typizowani klienci HTTP) |
| **Baza danych** | Azure SQL, Cosmos DB (NoSQL) |
| **DevOps** | GitHub Actions, Azure App Service, Docker (planowane) |
| **Architektura** | Clean Architecture, DDD, CQRS |

---

## 1. Opis Projektu  
System pobiera i przechowuje dane o kursach walut z API **NBP (Narodowego Banku Polskiego)** oraz udostępnia prosty interfejs webowy w technologii **Blazor**, który prezentuje najnowszą zsynchronizowaną tabelę kursów.
Warstwa frontendowa została zaimplementowana w **Blazor Web App** z wykorzystaniem **BlazorBootstrap**.
Podczas realizacji zadania był prowadzony [Architecture Decision Record](docs/ADR.md)

W ramach naszego API w tle uruchomiony jest **IHostedService**, który jest odpowiedzialny za pobieranie danych z aktywnych zewnętrznych źródeł, których konfiguracja znajduje się w tabeli `rates.Source`.
Każde zdefiniowane źródło musi mieć zaimplementowaną **strategię** synchronizacji danych oraz **CRON** expression określający częstotliwość uruchamiania.
Jeśli źródło danych jest wciąż w **stanie** pobierania rekordów archiwalnych to job uruchamia się wg konfiguracji `SourceSyncArchiveCronExpression`. 
Jeśli wszystkie dane historyczne zostały zaciągnięte to dane źródło będzie uruchamiać się wg konfiguracji z tabeli `rates.Source`.
Nowa tabela B jest publikowana przez NBP w środy ok. 12/13. Dlatego ja uznałem, że job będzie uruchamiał się `"* * 16 3 * *"`, czyli o godz 16 w 3 dzień tygodnia.

API udostępnia metodę GET `/tables/last` - i zwraca ostatnią tabelę zapisaną przez system (źródło nie ma znaczenia)
Strona główna portalu wyświetla dane z tego requestu.

### Kluczowe Funkcjonalności
- Zadanie cykliczne synchronizujące historyczne oraz aktualne dane o kursach walut z NBP.  
- Wyświetlanie ostatniej pobranej tabeli kursów w aplikacji webowej.

---

## 2. Dostęp do Repozytorium i Konfiguracja Środowiska  

### Klonowanie repozytorium
```bash
git clone [MarketHub](https://github.com/Rawenvoys/MarketHub.git)
cd MarketHub
```

### Konfiguracja środowiska

1. Zaktualizuj appsettings.json lub user-secrets:

- Rates Api
    ```json
    {
        "NbpApi:BaseUri": "https://{host}/api",
        "ConnectionStrings:RatesDb": "Server=tcp:{serverName};Initial Catalog={databaseName};Persist Security Info=False;User ID={userId};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
        "RatesCosmos:ConnectionString": "AccountEndpoint={accountEndpoint};AccountKey={accountKey}",
        "RatesCosmos:DatabaseId": "{ratesCosmosDatabaseId}"
    }
    ```

- Currency Portal Web
    ```json
    {
        "NbpApi:BaseUri": "https://{host}/api"
    }
    ```

- Rates Tools
    ```json
    {
        "ConnectionStrings:RatesDb": "Server=tcp:{serverName};Initial Catalog={databaseName};Persist Security Info=False;User ID={userId};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
    }
    ```

2. Zastosuj migracje EF Core:

- Tworzenie migracji i aktualizacja bazy   

    ```
    cd docs/Microservices/Rates/Tools 
    ./AddMigration.sh
    ```

### Uruchomienie projektu lokalnie

    ```bash
    cd src/Microservices/Rates/Api
    dotnet run
    ```

    ```bash
    cd src/Apps/CurrencyPortal/Web
    dotnet watch
    ```



## 3. Architektura i Działanie  

### Przegląd architektury
Projekt wykorzystuje **Clean Architecture** i **DDD (Domain-Driven Design)** z zastosowaniem wzorca **CQRS** w warstwie `Application`.

**Główne warstwy:**
- **Domain** – encje, agregaty (`Source`, `Table`, `CurrencyRate`) i obiekty wartości.  
- **Application** – zapytania (`GetLastTable`, `GetMeta`), interfejsy i logika aplikacyjna.  
- **Infrastructure** – konfiguracja EF Core, implementacje repozytoriów, obsługa Cosmos DB.  
- **API** – Minimal API w .NET 8, eksponujące endpointy do pobierania danych i uruchamiania synchronizacji.  
- **Clients** – klienci HTTP zbudowani z użyciem **Refit** (NBP, Rates).  
- **Tools** – projekt narzędziowy do migracji.  
- **Web (CurrencyPortal)** – aplikacja Blazor Web App z wykorzystaniem Bootstrap Blazor.
- **Hangfire** zarządza cyklicznymi zadaniami synchronizacji.  
- Dane są przechowywane w **Azure SQL** - dla danych dotyczących tabel/walut/kursów i **Azure Cosmos DB** - dla danych dotyczących synchronizacji o zmiennej strukturze

---

## 4. Planowane Usprawnienia  

| Obszar | Usprawnienie |
|---------|--------------|
| **Feature** | Narzędzie do importowania pliku **CSV** z listą transakcji i obliczania wartości **przychodu** oraz **kosztów uzyskania przychodu** ze sprzedaży **Akcji / ETF / Kryptowalut** lub uzyskania dywidend w walucie obcej |
| **Feature** | Dodanie implementacji domeny dla encji `SyncState`. Wyemitowanie oraz obsłużenie `ArchiveSynchronizationCompletedEvent` w celu przepięcia CRON na raz w tygodniu w środę o 16 |
| **Feature** | Przeglądanie wartości historycznych tabel za pomocą filtrów (**Rodzaj** i nazwa tabeli, **rok**, **miesiąc**, **tabela**)|
| **Feature** | Dodanie widoku przeglądania listy wszystkich tabel |
| **Feature** | Wdrożenie **filtrowania**, **sortowania** i **paginacji** po stronie serwera w widoku przeglądu listy tabel |
| **Feature** | Dodanie możliwości przeglądania historii wartości wybranej **waluty** w czasie z listy dostępnych walut wraz z **wykresem** |
| **Maintence** | Implementacja testów jednostkowych |
| **Maintence** | Implementacja testów integracyjnych |
| **Maintence** | Dokumentacja techniczna API |
| **DevOps** | Dodanie wsparcia dla Dockera do lokalnego uruchamiania. |
| **DevOps** | Konfiguracja CI/CD z wykorzystaniem GitHub Actions i wdrożeniem do Azure App Service. |

---

## 5. Struktura repozytorium

<details>

<summary>Struktura repozytorium</summary>

```
.
|-- docs
|   |-- ADR.md
|   |-- Business\ criteria.md
|   |-- Clients
|   |   `-- Nbp
|   |       `-- Client
|   |           `-- nbpapi.http
|   |-- Duplicates_NBP.sql
|   `-- Microservices
|       `-- Rates
|           `-- Tools
|               `-- AddMigration.sh
|-- MarketHub.slnx
|-- README.md
`-- src
    |-- Apps
    |   `-- CurrencyPortal
    |       |-- Application
    |       |   `-- Application.csproj
    |       `-- Web
    |           |-- appsettings.Development.json
    |           |-- appsettings.json
    |           |-- Components
    |           |-- _Imports.razor
    |           |-- Layout
    |           |   |-- App.razor
    |           |   |-- MainLayout.razor
    |           |   `-- MainLayout.razor.css
    |           |-- Pages
    |           |   |-- Error.razor
    |           |   |-- Home.razor
    |           |   `-- Home.razor.cs
    |           |-- Program.cs
    |           |-- Properties
    |           |   `-- launchSettings.json
    |           |-- Routes.razor
    |           |-- Web.csproj
    |           `-- wwwroot
    |               `-- app.css
    |-- Clients
    |   |-- Nbp
    |   |   |-- Client
    |   |   |   |-- Client.csproj
    |   |   |   |-- Extensions
    |   |   |   |   `-- ServiceCollectionExtensions.cs
    |   |   |   |-- GlobalUsings.cs
    |   |   |   |-- IExchangeRatesTablesApi.cs
    |   |   |   |-- INbpApi.cs
    |   |   |   `-- Options
    |   |   |       `-- NbpApiOptions.cs
    |   |   `-- Contracts
    |   |       |-- Contracts.csproj
    |   |       |-- Dtos
    |   |       |   `-- ExchangeRates
    |   |       |       `-- Tables
    |   |       |           |-- ExchangeRateDto.cs
    |   |       |           `-- ExchangeRateTableDto.cs
    |   |       `-- GlobalUsings.cs
    |   `-- Rates
    |       |-- Client
    |       |   |-- Client.csproj
    |       |   |-- Extensions
    |       |   |   `-- ServiceCollectionExtensions.cs
    |       |   |-- GlobalUsings.cs
    |       |   |-- IMetaApi.cs
    |       |   |-- IRatesApi.cs
    |       |   |-- ITablesApi.cs
    |       |   `-- Options
    |       |       `-- RatesApiOptions.cs
    |       `-- Contracts
    |           |-- Contracts.csproj
    |           `-- Dtos
    |               |-- GetLastTable
    |               |   |-- CurrencyRateDto.cs
    |               |   `-- TableDto.cs
    |               `-- GetMetaQuery
    |                   |-- MetaDto.cs
    |                   |-- SourceDto.cs
    |                   `-- TimeframeDto.cs
    `-- Microservices
        `-- Rates
            |-- Api
            |   |-- Api.csproj
            |   |-- Api.http
            |   |-- appsettings.Development.json
            |   |-- appsettings.json
            |   |-- Program.cs
            |   `-- Properties
            |       `-- launchSettings.json
            |-- Application
            |   |-- App.cs
            |   |-- Application.csproj
            |   |-- Extensions
            |   |   |-- LoggingBuilderExtensions.cs
            |   |   `-- ServiceCollectionExtensions.cs
            |   |-- Interfaces
            |   |   |-- IActualSourceSyncService.cs
            |   |   |-- IArchiveSourceSyncService.cs
            |   |   |-- IJobManager.cs
            |   |   |-- ISourceSyncJobManager.cs
            |   |   `-- ISourceSyncService.cs
            |   |-- Queries
            |   |   |-- GetLastTable
            |   |   |   |-- Handler.cs
            |   |   |   |-- Query.cs
            |   |   |   `-- Result.cs
            |   |   `-- GetMeta
            |   |       |-- Handler.cs
            |   |       |-- Query.cs
            |   |       `-- Result.cs
            |   `-- Services
            |       |-- ActualSourceSyncService.cs
            |       |-- ArchiveSourceSyncService.cs
            |       |-- JobManager.cs
            |       `-- SourceSyncJobManager.cs
            |-- Domain
            |   |-- Aggregates
            |   |   |-- Source.cs
            |   |   `-- Table.cs
            |   |-- Domain.csproj
            |   |-- Entities
            |   |   |-- Currency.cs
            |   |   `-- CurrencyRate.cs
            |   |-- Enums
            |   |   |-- Source
            |   |   |   |-- Month.cs
            |   |   |   |-- Status.cs
            |   |   |   `-- SyncStrategy.cs
            |   |   `-- Table
            |   |       `-- Type.cs
            |   |-- Exceptions
            |   |-- Extensions
            |   |   `-- ServiceCollectionExtensions.cs
            |   |-- GlobalUsings.cs
            |   |-- Interfaces
            |   |   |-- Factories
            |   |   |   `-- ISyncStrategyFactory.cs
            |   |   |-- Repositories
            |   |   |   |-- ISourceRepository.cs
            |   |   |   |-- ISyncStateRepository.cs
            |   |   |   `-- ITableRepository.cs
            |   |   |-- States
            |   |   |   `-- ISyncState.cs
            |   |   `-- Strategies
            |   |       `-- ISyncStrategy.cs
            |   `-- ValueObjects
            |       |-- Currency
            |       |   |-- Code.cs
            |       |   `-- Name.cs
            |       |-- CurrencyRate
            |       |   `-- Rate.cs
            |       |-- Source
            |       |   |-- CronExpression.cs
            |       |   |-- Name.cs
            |       |   |-- Timeframe.cs
            |       |   `-- Year.cs
            |       `-- Table
            |           `-- Number.cs
            |-- Infrastructure
            |   |-- Extensions
            |   |   |-- ConfigurationExtensions.cs
            |   |   `-- ServiceCollectionExtensions.cs
            |   |-- Factories
            |   |   `-- SyncStrategyFactory.cs
            |   |-- Infrastructure.csproj
            |   |-- Options
            |   |   `-- RatesCosmosOptions.cs
            |   |-- Persistance
            |   |   |-- Configurations
            |   |   |   |-- CurrencyConfiguration.cs
            |   |   |   |-- CurrencyRateConfiguration.cs
            |   |   |   |-- SourceConfiguration.cs
            |   |   |   `-- TableConfiguration.cs
            |   |   |-- Contexts
            |   |   |   `-- RatesDbContext.cs
            |   |   |-- Converters
            |   |   |   `-- CronExpressionConverter.cs
            |   |   |-- Extensions
            |   |   |   `-- ServiceCollectionExtensions.cs
            |   |   |-- Migrations
            |   |   |   |-- 20251024102636_MigrateDb.cs
            |   |   |   |-- 20251024102636_MigrateDb.Designer.cs
            |   |   |   |-- 20251024130802_RemoveIndexFromCurrencyRate.cs
            |   |   |   |-- 20251024130802_RemoveIndexFromCurrencyRate.Designer.cs
            |   |   |   |-- 20251024161540_AddUniqueIndexAndDecimalPrec.cs
            |   |   |   |-- 20251024161540_AddUniqueIndexAndDecimalPrec.Designer.cs
            |   |   |   `-- RatesDbContextModelSnapshot.cs
            |   |   |-- Repositories
            |   |   |   |-- SourceRepository.cs
            |   |   |   |-- SyncStateRepository.cs
            |   |   |   `-- TableRepository.cs
            |   |   |-- Seeds
            |   |   |   |-- SourceSeeder.cs
            |   |   |   `-- SyncStateSeeder.cs
            |   |   `-- States
            |   |       `-- NbpApiDateRangeSyncState.cs
            |   `-- Strategies
            |       `-- NbpApiDateRangeSyncStrategy.cs
            `-- Tools
                |-- Program.cs
                `-- Tools.csproj
```

</details>