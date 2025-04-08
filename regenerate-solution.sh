#!/bin/bash

# This script regenerates the solution file for FlagMyCountry

# Navigate to the src directory
cd "$(dirname "$0")/src" || exit 1

echo "Removing old solution file..."
rm -f FlagMyCountry.sln

echo "Creating new solution..."
dotnet new sln -n FlagMyCountry

echo "Adding project to solution..."
if [ -f "FlagMyCountry/FlagMyCountry.csproj" ]; then
  dotnet sln add FlagMyCountry/FlagMyCountry.csproj
  echo "Successfully added project to solution!"
else
  echo "ERROR: Project file not found at expected location"
  find . -name "*.csproj" -type f
  exit 1
fi

echo "Solution regenerated successfully!"
echo "You can now build the project with: dotnet build src/FlagMyCountry.sln"
