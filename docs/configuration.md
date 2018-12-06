# Configuration File

The configuration yaml file controls various settings on how the source files are generated.  The default configuration file name is `generation.yml`.

## Variables

The configuration file support self referencing variables. Variables are case insensitive.  See reference below for usage.

## Reference

```YAML
#---------------------------------#
# project section -  Used for shared variables through out the configuration file
#---------------------------------#
project:
  # the root namespace for the project
  namespace: 'Company.{Database.Name}'
  # the root directory for the project
  directory: .\

#---------------------------------#
# data section -  Used for configuring database connections
#---------------------------------#
database:
  # the connection string to the database
  connectionString: 'Data Source=(local);Initial Catalog=Tracker;Integrated Security=True'
  # the database provider name.  Default: SqlServer
  provider: SqlServer|PostgreSQL|MySQL|Sqlite

  # config name to read the connection string from the user secrets file
  connectionName: 'ConnectionStrings:Generator'
  # the user secret identifier, can be shared with .net core project
  userSecretsId: '984ef0cf-2b22-4fd1-876d-e01499da4c1f'
  
  # tables to include or empty to include all
  tables:
    - Priority
    - Status
    - Task
    - User

  # schemas to include or empty to include all
  schemas:
    - dbo

  # table naming hint for how existing tables are named.  Default: Singular
  tableNaming: Mixed|Plural|Singular

#---------------------------------#
# data section - controls the generated files for Entity Framework
#---------------------------------#
data:
  # data context file configuration
  context:
    name: '{Database.Name}Context'          # the data context class name
    baseClass: DbContext                    # the data context base class name
    namespace: '{Project.Namespace}.Data'   # the data context namespace
    directory: '{Project.Directory}\Data'   # the data context output directory

    # how to generate names for the DbSet properties on the data context.  Default: Plural
    propertyNaming: Preserve|Plural|Suffix
    #include XML documentation
    document: false

  # entity class file configuration
  entity:
    namespace: '{Project.Namespace}.Data.Entities'  # the entity class namespace
    directory: '{Project.Directory}\Data\Entities'  # the entity class output directory

    # how to generate entity class names from the table name.  Default: Singular
    entityNaming: Preserve|Plural|Singular

    # how to generate relationship collections names for the entity.  Default: Plural
    relationshipNaming: Preserve|Plural|Suffix
    #include XML documentation
    document: false

  # mapping class file configuration
  mapping:
    namespace: '{Project.Namespace}.Data.Mapping'   # the mapping class namespace
    directory: '{Project.Directory}\Data\Mapping'   # the mapping class output directory
    #include XML documentation
    document: false

  # query extension class file configuration
  query:
    generate: true          # generate query extension class files
    indexPrefix: By         # Prefix for queries built from an index
    uniquePrefix: GetBy     # Prefix for queries built from unique indexes
    namespace: '{Project.Namespace}.Data.Queries'   # the mapping class namespace
    directory: '{Project.Directory}\Data\Queries'   # the mapping class output directory
    #include XML documentation
    document: false

#---------------------------------#
# model section - controls the optional view model generation
#---------------------------------#
model:
  # shared options between read, create and update models
  shared:
    namespace: '{Project.Namespace}.Domain.Models' # the model class namespace
    directory: '{Project.Directory}\Domain\Models' # the mapping class output directory
    # regular expression of entities and properties to exclude in all models
    exclude:
      # list of regular expressions of entity names
      entities:
        - 'EmailDelivery'
        - 'UserLogin'

      # list of regular expressions of property names, source is Entity.Property
      properties:
        - 'User\.PasswordHash$'
        - 'User\.ResetHash$'

  # read view model class configuration
  read:
    generate: true                  # generate read model class files
    name: '{Entity.Name}ReadModel'  # the read model class name
    baseClass: EntityReadModel      # the read model base class
    namespace: '{Project.Namespace}.Domain.Models'
    directory: '{Project.Directory}\Domain\Models'
    exclude:
      entities: []
      properties: []

  # create view model class configuration
  create:
    generate: true                      # generate create model class files
    name: '{Entity.Name}CreateModel'    # the create model class name
    baseClass: EntityCreateModel        # the create model base class
    namespace: '{Project.Namespace}.Domain.Models'
    directory: '{Project.Directory}\Domain\Models'
    exclude:
      entities: []
      properties: []
  
  # update view model class configuration
  update:
    generate: true                      # generate update model class files
    name: '{Entity.Name}UpdateModel'    # the update model class name
    baseClass: EntityUpdateModel        # the update model base class
    namespace: '{Project.Namespace}.Domain.Models'
    directory: '{Project.Directory}\Domain\Models'
    exclude:
      entities: []
      properties: []

  # AutoMapper class configuration
  mapper:
    generate: true
    name: '{Entity.Name}Profile'
    baseClass: Profile
    namespace: '{Project.Namespace}.Domain.Mapping'
    directory: '{Project.Directory}\Domain\Mapping'
  
  # FluentValidation class configuration
  validator:
    generate: true
    name: '{Model.Name}Validator'
    baseClass: 'AbstractValidator<{Model.Name}>'
    namespace: '{Project.Namespace}.Domain.Validation'
    directory: '{Project.Directory}\Domain\Validation'

```