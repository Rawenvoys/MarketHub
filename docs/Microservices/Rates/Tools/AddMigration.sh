#!/bin/bash
echo "=========================="
echo "=== Add Migration Tool ==="
echo "=========================="
echo ""

cd ../../../../src/Microservices/Rates/Tools
read -p ">Enter migration name: " migrationName

if [ -z "$migrationName" ]; then
    echo "Migration name cannot be empty. Exiting."
    exit 1
fi

echo ">Adding migration: $migrationName"
dotnet ef migrations add $migrationName --project '../Infrastructure/Infrastructure.csproj' --output-dir '../Infrastructure/Persistance/Migrations'

echo ""

echo ">Applying migration: $migrationName"
dotnet ef database update --project '../Infrastructure/Infrastructure.csproj'

echo "Migration '$migrationName' added and database updated successfully."
echo "=========================="
