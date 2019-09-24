# Database Connection

Entity Framework Core Generator tool supports several different ways to connect to the database.

## Command Line Connection String

The database connection string can be passed in via command line using the `-c <ConnectionString>` argument.  Using the command line argument overrides all other connection strings.

## Configuration Yaml file

The connection string can be stored in the configuration yaml file under the `database/connectionString` section.

```YAML
database:
  connectionString: 'Data Source=(local);Initial Catalog=Tracker;Integrated Security=True'
```

Use the the `database/connectionString` configuration with caution.  You don't want to have a database user name and password stored in clear text when its committed to source control.

## User Secret Manager

Entity Framework Core Generator supports reading the connection string from the user secrets file.  The Secret Manager tool stores sensitive data in the user secrets file. User secrets are stored in a separate location from the project tree. The user secrets are associated with a specific user secret identifier. The user secrets aren't checked into source control.

Create a user secret with the `efg initialize` command.  The command will create a configuration yaml file if it doesn't exist, then it will update the user secret file with the supplied connection.  Finally it will update the configuration yaml with a `connectionName` and `userSecretsId` if they aren't set.

```Shell
efg initialize -c <ConnectionString>
```

To configure how Entity Framework Core Generator reads the user secret file, set the following in the configuration yaml file.

```YAML
database:
  connectionName: 'ConnectionStrings:Generator'
  userSecretsId: '984ef0cf-2b22-4fd1-876d-e01499da4c1f'
```

The connection string can also be store in the user secret file using the .NET Core `dotnet-user-secrets` nuget tool.

```Shell
dotnet user-secrets set "ConnectionStrings:Generator" "Data Source=(local);Initial Catalog=Tracker;Integrated Security=True" --id "984ef0cf-2b22-4fd1-876d-e01499da4c1f"
```

The `userSecretsId` can be shared with your .NET Core Project.

```XML
<PropertyGroup>
  <TargetFramework>netcoreapp2.1</TargetFramework>
  <UserSecretsId>984ef0cf-2b22-4fd1-876d-e01499da4c1f</UserSecretsId>
</PropertyGroup>
```

Read more about user secrets in the ASP.NET Core documentation

[Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?tabs=windows&view=aspnetcore-2.1#secret-manager)
