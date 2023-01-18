using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFrameworkCore.Generator.Core.Tests.Templates;

public class ModelYamlScript
{
    // simulate global script variables
    public TemplateOptions TemplateOptions { get; set; }

    public GeneratorOptions GeneratorOptions { get; set; }

    public IndentedStringBuilder CodeBuilder { get; set; }

    public Model Model { get; set; }



    public string WriteCode()
    {
        CodeBuilder.Clear();

        CodeBuilder.Append("ModelClass: ").Append(Model.ModelClass).AppendLine();
        CodeBuilder.Append("ModelType: ").Append(Model.ModelType.ToString()).AppendLine();
        CodeBuilder.Append("ModelNamespace: '").Append(Model.ModelNamespace).AppendLine("'");
        CodeBuilder.Append("ModelBaseClass: ").Append(Model.ModelBaseClass).AppendLine();
        CodeBuilder.Append("ValidatorNamespace: '").Append(Model.ValidatorNamespace).AppendLine("'");
        CodeBuilder.Append("ValidatorClass: ").Append(Model.ValidatorClass).AppendLine();
        CodeBuilder.Append("ValidatorBaseClass: ").Append(Model.ValidatorBaseClass).AppendLine();

        using (CodeBuilder.Indent())
            GenerateProperties();

        return CodeBuilder.ToString();
    }

    private void GenerateProperties()
    {
        foreach (var property in Model.Properties)
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