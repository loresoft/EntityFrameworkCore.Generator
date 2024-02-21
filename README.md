# Overview

.NET Core command-line (CLI) tool to generate Entity Framework Core model from an existing database.

[![Build Project](https://github.com/loresoft/EntityFrameworkCore.Generator/actions/workflows/dotnet.yml/badge.svg)](https://github.com/loresoft/EntityFrameworkCore.Generator/actions/workflows/dotnet.yml)

[![NuGet Version](https://img.shields.io/nuget/v/EntityFrameworkCore.Generator.svg?style=flat-square)](https://www.nuget.org/packages/EntityFrameworkCore.Generator/)

## Features

- Entity Framework Core database first model generation
- Safe regeneration via region replacement
- Safe Renaming via mapping file parsing
- Optionally generate read, create and update models from entity
- Optionally generate validation and object mapper classes

## Documentation

Entity Framework Core Generator documentation is available via [Read the Docs](https://efg.loresoft.com)

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

The `generate` command will create the follow files and directory structure by default.  The root directory defaults to the current working directory.  Most of the output names and locations can be customized in the [configuration file](https://efg.loresoft.com/configuration/)

#### Data Context Output

The EntityFramework DbContext file will be created in the root directory.

#### Entities Output

The entities directory will contain the generated source file for entity class representing each table.

#### Mapping Output

The mapping directory contains a fluent mapping class to map each entity to its table.

## Initialize Command

The `initialize` command is used to create the configuration yaml file and optionally set the [connection string](https://efg.loresoft.com/connectionString/). The configuration file has many options to configure the generated output.  See the [configuration file](https://efg.loresoft.com/configuration/) documentation for more details.

The following command will create an initial `generation.yaml` configuration file as well as setting a user secret to store the connection string.

```Shell
efg initialize -c <ConnectionString>
```

When a `generation.yaml` configuration file exists, you can run `efg generate` in the same directory to generate the source using that configuration file.

## Regeneration

Entity Framework Core Generator supports safe regeneration via region replacement and source code parsing.  A typical workflow for a project requires many database changes and updates.  Being able to regenerate the entities and associated files is a huge time saver.

### Region Replacement

All the templates output a region on first generation.  On future regeneration, only the regions are replaced.  This keeps any other changes you've made to the source file.

Example of a generated entity class

```C#
public partial class Status
{
    public Status()
    {
        #region Generated Constructor
        Tasks = new HashSet<Task>();
        #endregion
    }

    #region Generated Properties
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset Created { get; set; }

    public string CreatedBy { get; set; }

    public DateTimeOffset Updated { get; set; }

    public string UpdatedBy { get; set; }

    public Byte[] RowVersion { get; set; }
    #endregion

    #region Generated Relationships
    public virtual ICollection<Task> Tasks { get; set; }
    #endregion
}
```

When the `generate` command is re-run, the `Generated Constructor`, `Generated Properties` and `Generated Relationships` regions will be replace with the current output of the template.  Any other changes outside those regions will be safe.

### Source Parsing

In order to capture and preserve Entity, Property and DbContext renames, the `generate` command parses any existing mapping and DbContext class to capture how things are named.  This allows you to use the full extend of Visual Studio's refactoring tools to rename things as you like.  Then, when regenerating, those changes won't be lost.

## Database Providers

Entity Framework Core Generator supports the following databases.

- SQL Server
- PostgreSQL
- MySQL
- Sqlite

The provider can be set via command line or via the [configuration file](https://efg.loresoft.com/configuration/).

Set via command line

```Shell
efg generate -c <ConnectionString> -p <Provider>
```

Set in configuration file

```YAML
database:
  connectionString: 'Data Source=(local);Initial Catalog=Tracker;Integrated Security=True'
  provider: SqlServer
```

## Database Schema

The database schema is loaded from the metadata model factory implementation of `IDatabaseModelFactory`.  Entity Framework Core Generator uses the implemented interface from each of the supported providers similar to how `ef dbcontext scaffold` works.

## View Models

Entity Framework Core Generator supports generating [Read](https://efg.loresoft.com/md/read/), [Create](https://efg.loresoft.com/md/create/) and [Update](https://efg.loresoft.com/md/update/) view models from an entity.  Many projects rely on view models to shape data.  The model templates can be used to quickly get the basic view models created.  The model templates also support regeneration so any database change can easily be sync'd to the view models.  

## Change Log

### Version 6.0

- upgrade to .net 8
- add option to turn off temporal table mapping
- add rowversion options, ByteArray|Long|ULong 
- add script template merge

### Version 5.0

- add support for navigation property renames
- update generated query methods to support CancellationToken
- add support for temporal tables
- add regex clean support for table and column names

### Version 4.5

- add support for nullable reference types

### Version 4.0

- upgrade projects to .net 6

### Version 3.1

- Breaking change to generated mapping constants
  - `TableName` is now `Table.Name`
  - `TableSchema` is now `Table.Schema`
  - `Column{Name}` is now `Columns.{Name}`
- add additional automapper for read model to update model

### Version 3.0

- Add `Table.Name` and `Table.Schema` variable support in yaml configuration.
- Add entity name option to control the name of the entity.  Leave blank to use previous generate logic. Set under the entity -> name yaml settings.
- Add exclude table support.  Set under the database -> exclude yaml settings.
- Include and Exclude expression can now be `exact` or `regex`.  When exact, will be direct string match.  When regex, will use regular expression to match. Default is regex for legacy support.

### Version 2.5

- Add external script template support
- Misc bug fixes

### Version 2.0

- Add support for Entity Framework Core 3.0
- Add provider support for PostgreSQL, MySQL and Sqlite
- Add View support

### Version 1.1

- Add alias to commands, can use `efg gen` for `efg generate` and `efg init` for `efg initialize`
- Fix bug where base class for Entity was placed in wrong location
- Fix misc sorting issue that caused needless source control changes
