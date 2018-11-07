# Overview

.NET Core command-line (CLI) tool to generate Entity Framework Core source files.

## Install

The Entity Framework Core Generator tool is available on nuget.org via package name `EntityFrameworkCore.Generator`.

To install EntityFrameworkCore.Generator, run the following command in the console

    dotnet tool install --global EntityFrameworkCore.Generator

More information about NuGet package available at
<https://nuget.org/packages/EntityFrameworkCore.Generator>

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
