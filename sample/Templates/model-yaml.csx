public string WriteCode()
{
    CodeBuilder.Clear();

    CodeBuilder.Append("ModelClass: ").Append(Model.ModelClass).AppendLine();
    CodeBuilder.Append("ModelType: ").Append(Model.ModelType).AppendLine();
    CodeBuilder.Append("ModelNamespace: '").Append(Model.ModelNamespace).AppendLine("'");
    CodeBuilder.Append("ModelBaseClass: ").Append(Model.ModelBaseClass).AppendLine();
    CodeBuilder.Append("ValidatorNamespace: '").Append(Model.ValidatorNamespace).AppendLine("'");
    CodeBuilder.Append("ValidatorClass: ").Append(Model.ValidatorClass).AppendLine();
    CodeBuilder.Append("ValidatorBaseClass: ").Append(Model.ValidatorBaseClass).AppendLine();

    CodeBuilder.Append("Properties:").AppendLine();
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
        CodeBuilder.Append("  DataType: ").Append(property.DataType).AppendLine();
        CodeBuilder.Append("  SystemType: ").Append(property.SystemType.Name).AppendLine();

        if (property.Size != null)
            CodeBuilder.Append("  Size: ").Append(property.Size).AppendLine();

        if (property.Default != null)
            CodeBuilder.Append("  Default: '").Append(property.Default).AppendLine("'");

        if (property.ValueGenerated != null)
            CodeBuilder.Append("  ValueGenerated: ").Append(property.ValueGenerated).AppendLine();

        if (property.IsNullable != null)
            CodeBuilder.Append("  IsNullable: ").Append(property.IsNullable).AppendLine();

        if (property.IsPrimaryKey != null)
            CodeBuilder.Append("  IsPrimaryKey: ").Append(property.IsPrimaryKey).AppendLine();

        if (property.IsForeignKey != null)
            CodeBuilder.Append("  IsForeignKey: ").Append(property.IsForeignKey).AppendLine();

        if (property.IsReadOnly != null)
            CodeBuilder.Append("  IsReadOnly: ").Append(property.IsReadOnly).AppendLine();

        if (property.IsRowVersion != null)
            CodeBuilder.Append("  IsRowVersion: ").Append(property.IsRowVersion).AppendLine();

        if (property.IsUnique != null)
            CodeBuilder.Append("  IsUnique: ").Append(property.IsUnique).AppendLine();
    }
}

// run script
WriteCode()