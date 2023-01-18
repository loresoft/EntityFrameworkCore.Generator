using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.Generator.Core.Tests.Templates;

public class EntityYamlScript
{
    // simulate global script variables
    public TemplateOptions TemplateOptions { get; set; }

    public GeneratorOptions GeneratorOptions { get; set; }

    public IndentedStringBuilder CodeBuilder { get; set; }

    public Entity Entity { get; set; }



    public string WriteCode()
    {
        CodeBuilder.Clear();

        CodeBuilder.Append("EntityClass: ").Append(Entity.EntityClass).AppendLine();
        CodeBuilder.Append("EntityNamespace: '").Append(Entity.EntityNamespace).AppendLine("'");
        CodeBuilder.Append("EntityBaseClass: ").Append(Entity.EntityBaseClass).AppendLine();

        CodeBuilder.Append("ContextProperty: ").Append(Entity.ContextProperty).AppendLine();

        CodeBuilder.Append("TableSchema: '").Append(Entity.TableSchema).AppendLine("'");
        CodeBuilder.Append("TableName: '").Append(Entity.TableName).AppendLine("'");


        CodeBuilder.Append("MappingClass: ").Append(Entity.MappingClass).AppendLine();
        CodeBuilder.Append("MappingNamespace: '").Append(Entity.MappingNamespace).AppendLine("'");

        CodeBuilder.Append("MapperClass: ").Append(Entity.MapperClass).AppendLine();
        CodeBuilder.Append("MapperNamespace: '").Append(Entity.MapperClass).AppendLine("'");
        CodeBuilder.Append("MapperBaseClass: ").Append(Entity.MapperBaseClass).AppendLine();

        CodeBuilder.Append("  IsView: ").Append(Entity.IsView.ToString()).AppendLine();

        using (CodeBuilder.Indent())
            GenerateProperties();

        return CodeBuilder.ToString();
    }

    private void GenerateProperties()
    {
        foreach (var property in Entity.Properties)
        {
            CodeBuilder.Append("- PropertyName: ").Append(property.PropertyName).AppendLine();
            CodeBuilder.Append("  ColumnName: '").Append(property.ColumnName).AppendLine("'");
            CodeBuilder.Append("  StoreType: ").Append(property.StoreType).AppendLine();
            CodeBuilder.Append("  NativeType: '").Append(property.NativeType).AppendLine("'");
            CodeBuilder.Append("  DataType: ").Append(property.DataType.ToString()).AppendLine();
            CodeBuilder.Append("  SystemType: ").Append(property.SystemType.Name).AppendLine();

            if (property.Size != null)
                CodeBuilder.Append("  Size: ").Append(property.Size?.ToString()).AppendLine();

            if (property.Default != null)
                CodeBuilder.Append("  Default: '").Append(property.Default).AppendLine("'");

            if (property.ValueGenerated != null)
                CodeBuilder.Append("  ValueGenerated: ").Append(property.ValueGenerated?.ToString()).AppendLine();

            if (property.IsNullable != null)
                CodeBuilder.Append("  IsNullable: ").Append(property.IsNullable?.ToString()).AppendLine();

            if (property.IsPrimaryKey != null)
                CodeBuilder.Append("  IsPrimaryKey: ").Append(property.IsPrimaryKey?.ToString()).AppendLine();

            if (property.IsForeignKey != null)
                CodeBuilder.Append("  IsForeignKey: ").Append(property.IsForeignKey?.ToString()).AppendLine();

            if (property.IsReadOnly != null)
                CodeBuilder.Append("  IsReadOnly: ").Append(property.IsReadOnly?.ToString()).AppendLine();

            if (property.IsRowVersion != null)
                CodeBuilder.Append("  IsRowVersion: ").Append(property.IsRowVersion?.ToString()).AppendLine();

            if (property.IsUnique != null)
                CodeBuilder.Append("  IsUnique: ").Append(property.IsUnique?.ToString()).AppendLine();
        }
    }

}