# Command Line Reference

Entity Framework Core Generator has the following commands

## Initialize Command

The `initialize` command is used to create the configuration yaml file and optionally set the [connection string](connectionString.md). The configuration file has many options to configure the generated output.  See the [configuration file](configuration.md) documentation for more details.

```Shell
Usage: efg initialize [options]

Options:
  -h, --help                              Show help information
  -p, --provider <PROVIDER>               Database provider to reverse engineer
  -c, --connection-string <CONNECTION_STRING>
                                          Database connection string to reverse engineer
  --id <USER_SECRETS_ID>                  The user secret ID to use.
  --name <CONNECTION_NAME>                The user secret configuration name.
  -d, --directory <DIRECTORY>             The root working directory
  -f, --file <FILE>                       The options file name

Example:

efg initialize -c "Data Source=(local);Initial Catalog=Tracker;Integrated Security=True"
```

## Generate Command

The `generate` command creates source code files from a database schema.  Running the command without any options will generate based on the configuration yaml file settings. Options pass via command line override values in the configuration yaml file.

```Shell
Usage: efg generate [options]

Options:
  -h, --help                            Show help information
  -p, --provider <PROVIDER>             Database provider to reverse engineer
  -c, --connection-string <CONNECTION_STRING>
                                        Database connection string to reverse engineer
  --extensions                          Include query extensions in generation
  --models                              Include view models in generation
  --mapper                              Include object mapper in generation
  --validator                           Include model validation in generation
  -d, --directory <DIRECTORY>           The root working directory
  -f, --file <FILE>                     The options file name

Example:

efg generate
```
