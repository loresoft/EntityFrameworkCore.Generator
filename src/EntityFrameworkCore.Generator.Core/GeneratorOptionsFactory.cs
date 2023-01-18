using System;
using System.Threading;
using EntityFrameworkCore.Generator.Options;
using YamlDotNet.Serialization;

namespace EntityFrameworkCore.Generator;

public class GeneratorOptionsFactory : IObjectFactory
{
    private readonly GeneratorOptions _generatorOptions;
    private int _scriptCount;

    public GeneratorOptionsFactory()
    {
        _generatorOptions = new GeneratorOptions();
        _generatorOptions.Variables.ShouldEvaluate = false;
        _scriptCount = 0;
    }

    public object Create(Type type)
    {
        // work around YamlDotNet requiring parameterless constructor
        if (type == typeof(GeneratorOptions))
            return _generatorOptions;

        if (type == typeof(ProjectOptions))
            return _generatorOptions.Project;
        if (type == typeof(DatabaseOptions))
            return _generatorOptions.Database;
        if (type == typeof(DataOptions))
            return _generatorOptions.Data;
        if (type == typeof(ModelOptions))
            return _generatorOptions.Model;
        if (type == typeof(ScriptOptions))
            return _generatorOptions.Script;

        if (type == typeof(ContextClassOptions))
            return _generatorOptions.Data.Context;
        if (type == typeof(EntityClassOptions))
            return _generatorOptions.Data.Entity;
        if (type == typeof(MappingClassOptions))
            return _generatorOptions.Data.Mapping;
        if (type == typeof(QueryExtensionOptions))
            return _generatorOptions.Data.Query;

        if (type == typeof(SharedModelOptions))
            return _generatorOptions.Model.Shared;
        if (type == typeof(ReadModelOptions))
            return _generatorOptions.Model.Read;
        if (type == typeof(CreateModelOptions))
            return _generatorOptions.Model.Create;
        if (type == typeof(UpdateModelOptions))
            return _generatorOptions.Model.Update;
        if (type == typeof(MapperClassOptions))
            return _generatorOptions.Model.Mapper;
        if (type == typeof(ValidatorClassOptions))
            return _generatorOptions.Model.Validator;

        if (type == typeof(TemplateOptions))
        {
            var index = Interlocked.Increment(ref _scriptCount);
            var prefix = $"Script{index}";

            return new TemplateOptions(_generatorOptions.Variables, prefix);
        }

        return Activator.CreateInstance(type);
    }
}