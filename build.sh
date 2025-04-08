#!/bin/bash

# Navigate to the source directory
cd src

# Clean previous builds
rm -rf FlagMyCountry/bin FlagMyCountry/obj

# Restore dependencies
dotnet restore FlagMyCountry.sln

# Build the solution
dotnet build FlagMyCountry.sln -c Release

# Output location
echo "Plugin DLL built at: src/FlagMyCountry/bin/Release/netstandard2.0/FlagMyCountry.dll"
