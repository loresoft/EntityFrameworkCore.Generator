using System;

using FluentAssertions;

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
        result.Should().Be("Tester.Core");
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
        result.Should().Be("Tester.Core.Data");
    }

    [Fact]
    public void MissingVariable()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Project.Namespace", "{Database.Name}.Core");

        var result = dictionary.Get("Project.Namespace");
        result.Should().Be(".Core");
    }

    [Fact]
    public void FormatExceptionWithNoEndFormatBrace()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "Tester");
        dictionary.Set("Project.Namespace", "{Database.Name.Core");

        Action action = () => dictionary.Get("Project.Namespace");
        action.Should().Throw<FormatException>();
    }


    [Fact]
    public void FormatExceptionWithEscapedEndFormatBrace()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "Tester");
        dictionary.Set("Project.Namespace", "{Database.Name}}.Core");

        Action action = () => dictionary.Get("Project.Namespace");
        action.Should().Throw<FormatException>();
    }


    [Fact]
    public void EvaluationWithEscapedBrace()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "Tester");
        dictionary.Set("Project.Namespace", "{{{Database.Name}.Core}}");

        var result = dictionary.Get("Project.Namespace");
        result.Should().Be("{Tester.Core}");
    }

    [Fact]
    public void InfiniteLoop()
    {
        var dictionary = new VariableDictionary();

        dictionary.Set("Database.Name", "{Project.Name}");
        dictionary.Set("Project.Name", "{Database.Name}");
        dictionary.Set("Project.Namespace", "{Database.Name}.Core");

        var result = dictionary.Get("Project.Namespace");
        result.Should().Be(".Core");
    }
}