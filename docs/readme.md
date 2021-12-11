# Overview

.NET Core command-line (CLI) tool to generate Entity Framework Core model from an existing database.

## Features

- Entity Framework Core database first model generation
- Safe regeneration via region replacement
- Safe Renaming via mapping file parsing
- Optionally generate read, create and update models from entity
- Optionally generate validation and object mapper classes

## Documentation

Entity Framework Core Generator documentation is available via [Read the Docs](https://efg.loresoft.com)

## Initialize Command

The `initialize` command is used to create the configuration yaml file and optionally set the [connection string](https://efg.loresoft.com/en/latest/connectionString/). The configuration file has many options to configure the generated output.  See the [configuration file](https://efg.loresoft.com/en/latest/configuration/) documentation for more details.

The following command will create an initial `generation.yaml` configuration file as well as setting a user secret to store the connection string.

```Shell
efg initialize -c <ConnectionString>
```

Replace `<ConnectionString>` with a valid database connection string.

When a `generation.yaml` configuration file exists, you can run `efg generate` in the same directory to generate the source using that configuration file.

## Generate Command

Entity Framework Core Generator (efg) creates source code files from a database schema. To generate the files with the `generation.yaml` configuration, run the following

```Shell
efg generate
```

## Regeneration

Entity Framework Core Generator supports safe regeneration via region replacement and source code parsing.  A typical workflow for a project requires many database changes and updates.  Being able to regenerate the entities and associated files is a huge time saver.

### Region Replacement

All the templates output a region on first generation.  On future regeneration, only the regions are replaced.  This keeps any other changes you've made to the source file.

### Source Parsing

In order to capture and preserve Entity, Property and DbContext renames, the `generate` command parses any existing mapping and DbContext class to capture how things are named.  This allows you to use the full extend of Visual Studio's refactoring tools to rename things as you like.  Then, when regenerating, those changes won't be lost.

## Database Providers

Entity Framework Core Generator supports the following databases.

- SQL Server
- PostgreSQL
- MySQL
- Sqlite
- Oracle

The provider can be set via command line or via the [configuration file](https://efg.loresoft.com/en/latest/configuration/).

## View Models

Entity Framework Core Generator supports generating [Read](https://efg.loresoft.com/en/latest/md/read/), [Create](https://efg.loresoft.com/en/latest/md/create/) and [Update](https://efg.loresoft.com/en/latest/md/update/) view models from an entity.  Many projects rely on view models to shape data.  The model templates can be used to quickly get the basic view models created.  The model templates also support regeneration so any database change can easily be sync'd to the view models.  
