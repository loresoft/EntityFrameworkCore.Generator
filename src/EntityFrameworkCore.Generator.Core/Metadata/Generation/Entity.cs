using System.Diagnostics;

namespace EntityFrameworkCore.Generator.Metadata.Generation;

/// <summary>
/// An entity model for a database table used when reverse engineering an existing database.
/// </summary>
/// <seealso cref="ModelBase" />
[DebuggerDisplay("Class: {EntityClass}, Table: {TableName}, Context: {ContextProperty}")]
public class Entity : ModelBase, IOptionVariable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    public Entity()
    {
        Properties = new PropertyCollection();
        Relationships = new RelationshipCollection();
        Methods = new MethodCollection();
        Models = new ModelCollection();
    }

    /// <summary>
    /// Gets or sets the parent <see cref="EntityContext"/> this entity belong to.
    /// </summary>
    /// <value>
    /// The parent context this entity belongs to.
    /// </value>
    public EntityContext Context { get; set; }

    /// <summary>
    /// Gets or sets the property name for this entity on the data context.
    /// </summary>
    /// <value>
    /// The property name for this entity on the data context.
    /// </value>
    public string ContextProperty { get; set; }


    /// <summary>
    /// Gets or sets the entity namespace.
    /// </summary>
    /// <value>
    /// The entity namespace.
    /// </value>
    public string EntityNamespace { get; set; }

    /// <summary>
    /// Gets or sets the name of the entity class.
    /// </summary>
    /// <value>
    /// The name of the entity class.
    /// </value>
    public string EntityClass { get; set; }

    /// <summary>
    /// Gets or sets the entity base class.
    /// </summary>
    /// <value>
    /// The entity base class.
    /// </value>
    public string EntityBaseClass { get; set; }


    /// <summary>
    /// Gets or sets the mapping namespace.
    /// </summary>
    /// <value>
    /// The mapping namespace.
    /// </value>
    public string MappingNamespace { get; set; }

    /// <summary>
    /// Gets or sets the name of the table mapping class.
    /// </summary>
    /// <value>
    /// The name of the table mapping class.
    /// </value>
    public string MappingClass { get; set; }


    /// <summary>
    /// Gets or sets the mapper class.
    /// </summary>
    /// <value>
    /// The mapper class.
    /// </value>
    public string MapperClass { get; set; }

    /// <summary>
    /// Gets or sets the mapper namespace.
    /// </summary>
    /// <value>
    /// The mapper namespace.
    /// </value>
    public string MapperNamespace { get; set; }

    /// <summary>
    /// Gets or sets the mapper base class.
    /// </summary>
    /// <value>
    /// The mapper base class.
    /// </value>
    public string MapperBaseClass { get; set; }


    /// <summary>
    /// Gets or sets the table schema.
    /// </summary>
    /// <value>
    /// The table schema.
    /// </value>
    public string TableSchema { get; set; }

    /// <summary>
    /// Gets or sets the name of the table.
    /// </summary>
    /// <value>
    /// The name of the table.
    /// </value>
    public string TableName { get; set; }


    /// <summary>
    /// Gets or sets the entity's properties.
    /// </summary>
    /// <value>
    /// The entity's properties.
    /// </value>
    public PropertyCollection Properties { get; set; }

    /// <summary>
    /// Gets or sets the entity's relationships.
    /// </summary>
    /// <value>
    /// The entity's relationships.
    /// </value>
    public RelationshipCollection Relationships { get; set; }

    /// <summary>
    /// Gets or sets the entity's methods.
    /// </summary>
    /// <value>
    /// The entity's methods.
    /// </value>
    public MethodCollection Methods { get; set; }


    /// <summary>
    /// Gets or sets the models for this entity.
    /// </summary>
    /// <value>
    /// The models for this entity.
    /// </value>
    public ModelCollection Models { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is view.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is view; otherwise, <c>false</c>.
    /// </value>
    public bool IsView { get; set; }


    /// <summary>
    /// Gets or sets the name of the temporal table.
    /// </summary>
    /// <value>
    /// The name of the temporal table.
    /// </value>
    public string TemporalTableName { get; set; }

    /// <summary>
    /// Gets or sets the temporal table schema.
    /// </summary>
    /// <value>
    /// The temporal table schema.
    /// </value>
    public string TemporalTableSchema { get; set; }

    /// <summary>
    /// Gets or sets the temporal start property.
    /// </summary>
    /// <value>
    /// The temporal start property.
    /// </value>
    public string TemporalStartProperty { get; set; }

    /// <summary>
    /// Gets or sets the temporal start column.
    /// </summary>
    /// <value>
    /// The temporal start column.
    /// </value>
    public string TemporalStartColumn { get; set; }

    /// <summary>
    /// Gets or sets the temporal end property.
    /// </summary>
    /// <value>
    /// The temporal end property.
    /// </value>
    public string TemporalEndProperty { get; set; }

    /// <summary>
    /// Gets or sets the temporal end column.
    /// </summary>
    /// <value>
    /// The temporal end column.
    /// </value>
    public string TemporalEndColumn { get; set; }

    void IOptionVariable.Set(VariableDictionary variableDictionary)
    {
        variableDictionary.Set(VariableConstants.TableSchema, TableSchema);
        variableDictionary.Set(VariableConstants.TableName, TableName);
        variableDictionary.Set(VariableConstants.EntityName, EntityClass);
    }

    void IOptionVariable.Remove(VariableDictionary variableDictionary)
    {
        variableDictionary.Remove(VariableConstants.TableSchema);
        variableDictionary.Remove(VariableConstants.TableName);
        variableDictionary.Remove(VariableConstants.EntityName);
    }
}
