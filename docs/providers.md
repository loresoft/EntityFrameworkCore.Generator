# Database Providers

Entity Framework Core Generator (efg) supports the following databases.

* SQL Server
* PostgreSQL
* MySQL
* Sqlite
* Oracle

## Database Schema

The database schema is loaded from [SchemaSaurus](https://github.com/loresoft/SchemaSaurus) for each supported provider.

## Usage

The provider can be set via command line or via the [configuration file](configuration.md).

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

Supported provider values are `SqlServer`, `PostgreSQL`, `MySQL`, `Sqlite` and `Oracle`.

### Oracle

Use `Oracle` as the provider value to generate from an Oracle database schema.

```YAML
database:
  connectionString: 'User Id=<User>;Password=<Password>;Data Source=<Host>:<Port>/<ServiceName>'
  provider: Oracle
```

Oracle provider support includes sequences and foreign keys. Schema filtering, identity columns, temporal tables and change tracking are not supported by the current Oracle provider defaults.
