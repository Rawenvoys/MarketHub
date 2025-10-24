#!/bin/bash
echo "=========================="
echo "=== Add Migration Tool ==="
echo "=========================="
echo ""

script_dir=$(cd "$(dirname "$0")" && pwd)
ls
src_dir="$script_dir/../../../src/Microservices/Rates"
cd "$src_dir"
ls

projectPath="$src_dir/Infrastructure/Infrastructure.csproj";
outputDirPath="$src_dir/Infrastructure/Persistence/Migrations";
startupProjectPath="$src_dir/Tools/Tools.csproj";

read -p ">Enter migration name: " migrationName

if [ -z "$migrationName" ]; then
  echo "Migration name cannot be empty. Exiting."
  exit 1
fi

echo ">Adding migration: $migrationName"
dotnet ef migrations add $migrationName \
    --project $projectPath \
    --startup-project $startupProjectPath \
    --output-dir $outputDirPath

echo "src/Microservices/Rates/Infrastructure/Infrastructure.csproj"
echo "$projectPath"
echo "src/Microservices/Rates/Tools/Tools.csproj"
echo "$startupProjectPath"
echo "src/Microservices/Rates/Infrastructure/Persistence/Migrations"
echo "$outputDirPath"

# dotnet ef database update --project `$projectWithDbContextPath` --startup-project `$startupProjectPath`
# echo "Migration '$migrationName' added and database updated successfully."