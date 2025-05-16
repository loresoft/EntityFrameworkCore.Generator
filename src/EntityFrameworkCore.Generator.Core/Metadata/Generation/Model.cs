namespace EntityFrameworkCore.Generator.Metadata.Generation;

public class Model : ModelBase, IOptionVariable
{
    public Model()
    {
        Properties = [];
    }

    public Entity Entity { get; set; } = null!;

    public ModelType ModelType { get; set; }

    public string ModelNamespace { get; set; } = null!;

    public string ModelClass { get; set; } = null!;

    public string? ModelBaseClass { get; set; }

    public string? ModelAttributes { get; set; }


    public string? ValidatorNamespace { get; set; }

    public string? ValidatorClass { get; set; }

    public string? ValidatorBaseClass { get; set; }


    public PropertyCollection Properties { get; set; }

    void IOptionVariable.Set(VariableDictionary variableDictionary)
    {
        variableDictionary.Set(VariableConstants.ModelName, ModelClass);
    }

    void IOptionVariable.Remove(VariableDictionary variableDictionary)
    {
        variableDictionary.Remove(VariableConstants.ModelName);
    }
}
