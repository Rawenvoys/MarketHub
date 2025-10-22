# $projectPath = '../../../../src/Microservices/Rates/Infrastructure/Infrastructure.csproj';
# $outputDir = '../../../../src/Microservices/Rates/Infrastructure/Tools/Migrations';
dotnet ef migrations add SourceEntity --project '../Infrastructure/Infrastructure.csproj' --output-dir '../Infrastructure/Persistance/Migrations'

# dotnet ef database update --project ../../../../src/Microservices/Rates/Infrastructure/Infrastructure.csproj