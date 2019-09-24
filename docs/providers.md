# Database Providers

Entity Framework Core Generator (efg) supports the following databases.

* SQL Server
* PostgreSQL
* MySQL
* Sqlite

## Database Schema

The database schema is loaded from the Entity Framework Core database metadata model factory implementation of `IDatabaseModelFactory`.  Entity Framework Core Generator used the the implemented interface from each of the supported providers similar to how `ef dbcontext scaffold` works.

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