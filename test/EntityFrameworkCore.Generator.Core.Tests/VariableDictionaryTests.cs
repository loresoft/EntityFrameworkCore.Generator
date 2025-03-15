using System;

using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests;

public class VariableDictionaryTests
{
    [Fact]
    public void Evaluation()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "Tester");
        dictionary.Set("Project.Namespace", "{Database.Name}.Core");

        var result = dictionary.Get("Project.Namespace");
        Assert.Equal("Tester.Core", result);
    }

    [Fact]
    public void NestedEvaluation()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "Tester");
        dictionary.Set("Project.Name", "{Database.Name}");
        dictionary.Set("Project.Namespace", "{Database.Name}.Core");
        dictionary.Set("Entity.Namespace", "{Project.Namespace}.Data");

        var result = dictionary.Get("Entity.Namespace");
        Assert.Equal("Tester.Core.Data", result);
    }

    [Fact]
    public void MissingVariable()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Project.Namespace", "{Database.Name}.Core");

        var result = dictionary.Get("Project.Namespace");
        Assert.Equal(".Core", result);
    }

    [Fact]
    public void FormatExceptionWithNoEndFormatBrace()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "Tester");
        dictionary.Set("Project.Namespace", "{Database.Name.Core");

        Action action = () => dictionary.Get("Project.Namespace");
        Assert.Throws<FormatException>(action);
    }


    [Fact]
    public void FormatExceptionWithEscapedEndFormatBrace()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "Tester");
        dictionary.Set("Project.Namespace", "{Database.Name}}.Core");

        Action action = () => dictionary.Get("Project.Namespace");
        Assert.Throws<FormatException>(action);
    }


    [Fact]
    public void EvaluationWithEscapedBrace()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "Tester");
        dictionary.Set("Project.Namespace", "{{{Database.Name}.Core}}");

        var result = dictionary.Get("Project.Namespace");
        Assert.Equal("{Tester.Core}", result);
    }

    [Fact]
    public void InfiniteLoop()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "{Project.Name}");
        dictionary.Set("Project.Name", "{Database.Name}");
        dictionary.Set("Project.Namespace", "{Database.Name}.Core");

        var result = dictionary.Get("Project.Namespace");
        Assert.Equal(".Core", result);
    }
}