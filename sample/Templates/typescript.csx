public string WriteCode()
{
    CodeBuilder.Clear();

    var hasNamespace = TemplateOptions.Namespace.HasValue();
    if (hasNamespace)
    {
        CodeBuilder
            .Append("namespace ")
            .Append(TemplateOptions.Namespace)
            .AppendLine(" {");

        CodeBuilder.IncrementIndent();
    }

    GenerateInterface();

    if (hasNamespace)
    {
        CodeBuilder.DecrementIndent();
        CodeBuilder.AppendLine("}");
    }

    return CodeBuilder.ToString();
}

private void GenerateInterface()
{
    var modelClass = Model.ModelClass.ToSafeName().ToCamelCase();

    CodeBuilder.Append($"export interface {modelClass}");

    if (TemplateOptions.BaseClass.HasValue())
    {
        var modelBase = TemplateOptions.BaseClass.ToSafeName().ToCamelCase();
        CodeBuilder.Append($" extends {modelBase}");
    }

    CodeBuilder.AppendLine(" {");

    using (CodeBuilder.Indent())
        GenerateProperties();

    CodeBuilder.AppendLine("}");
}

private void GenerateProperties()
{
    foreach (var property in Model.Properties)
    {
        var propertyType = ToScriptType(property.SystemType);
        var propertyName = property.PropertyName.ToSafeName().ToCamelCase();

        CodeBuilder.Append(propertyName);
        if (property.IsOptional)
            CodeBuilder.Append("?");

        CodeBuilder
            .Append(": ")
            .Append(propertyType)
            .AppendLine(";");
    }
}

private static string ToScriptType(Type type)
{
    var t = type.FullName;

    switch (t)
    {
        case "System.Int16":
        case "System.Int32":
        case "System.Byte":
        case "System.Double":
        case "System.SByte":
        case "System.Single":
        case "System.UInt16":
        case "System.UInt32":
            return "number";
        case "System.Decimal":
        case "System.Int64":
        case "System.UInt64":
            return "number";
        case "System.Boolean":
            return "boolean";
        case "System.DateTime":
        case "System.DateTimeOffset":
            return "Date";
        case "System.String":
        case "System.Guid":
        case "System.TimeSpan":
            return "string";
        default:
            return "any";
    }
}

// run script
WriteCode()