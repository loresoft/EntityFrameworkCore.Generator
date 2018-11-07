# Overview

.NET Core command-line (CLI) tool to generate Entity Framework Core source files.

[![Build status](https://ci.appveyor.com/api/projects/status/7ncldyr182jpu524?svg=true)](https://ci.appveyor.com/project/LoreSoft/entityframeworkcore-generator)

## Download

The Entity Framework Core Generator tool is available on nuget.org via package name `EntityFrameworkCore.Generator`.

To install EntityFrameworkCore.Generator, run the following command in the console

    dotnet tool install --global EntityFrameworkCore.Generator

More information about NuGet package available at
<https://nuget.org/packages/EntityFrameworkCore.Generator>

## Development Builds

Development builds are available on the myget.org feed.  A development build is promoted to the main NuGet feed when it's determined to be stable.

In your Package Manager settings add the following package source for development builds:
<http://www.myget.org/F/loresoft/>

## Features

- Entity Framework Core database first model generation
- Safe regeneration via region replacement
- Safe Renaming via mapping file parsing
- Optionally generate read, create and update models from entity
- Optionally generate validation and object mapper classes
- Optionally generate TypeScript definition files from view models

## Usage

To generate source code files from your database, use the generate command with your connection string.

    efg generate -c <ConnectionString>
