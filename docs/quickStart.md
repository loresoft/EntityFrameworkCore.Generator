# Quick Start

## Installation

To install EntityFrameworkCore.Generator tool, run the following command in the console

```Shell
dotnet tool install --global EntityFrameworkCore.Generator
```

After the tool has been install, the `efg` command line will be available.  Run `efg --help` for command line options

## Generate Command

Entity Framework Core Generator (efg) creates source code files from a database schema. To generate the files with no configuration, run the following

```Shell
efg generate -c <ConnectionString>
```

Replace `<ConnectionString>` with a valid database connection string.

### Generation Output

The `generate` command will create the follow files and directory structure by default.  The root directory defaults to the current working directory.  Most of the output names and locations can be customized in the [configuration file](configuration.md)

#### Entities Directory

The entities directory will contain the generated source file for entity class representing each table.

#### Mapping Directory

The mapping directory contains a fluent mapping class to map each entity to its table.

#### Data Context File

The EntityFramework DbContext file will be created in the root directory.

## Initialize Command

The `initialize` command is used to create the configuration yaml file and optionally set the [connection string](connectionString.md). The configuration file has many options to configure the generated output.  See the [configuration file](configuration.md) documentation for more details.

The following command will create an initial `generation.yaml` configuration file as well as setting a user secret to store the connection string.

```Shell
efg initialize -c <ConnectionString>
```

When a `generation.yaml` configuration file exists, you can run `efg generate` in the same directory to generate the source using that configuration file.