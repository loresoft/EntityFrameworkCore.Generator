using EntityFrameworkCore.Generator.Parsing;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests.Parsing;

public class ModelParserTests
{
    [Fact]
    public void ParseCodePropertyAttributes()
    {
        var parser = new ModelParser(NullLoggerFactory.Instance);

        var source = """
            using System.ComponentModel.DataAnnotations;

            namespace Tracker.Data.Models
            {
                public partial class UserReadModel
                {
                    [Required]
                    public string EmailAddress { get; set; }

                    [StringLength(100)]
                    [Display(Name = "Full Name")]
                    public string Name { get; set; }

                    public int Age { get; set; }
                }
            }
            """;

        var result = parser.ParseCode(source);

        Assert.NotNull(result);
        Assert.Equal("UserReadModel", result.ModelClass);
        Assert.Collection(
            result.Properties,
            property =>
            {
                Assert.Equal("EmailAddress", property.PropertyName);
                var attribute = Assert.Single(property.Attributes);
                Assert.Equal("[Required]", attribute);
            },
            property =>
            {
                Assert.Equal("Name", property.PropertyName);
                Assert.Equal(
                    ["[StringLength(100)]", "[Display(Name = \"Full Name\")]"],
                    property.Attributes);
            });
    }

    [Fact]
    public void ParseCodeMultiLinePropertyAttribute()
    {
        var parser = new ModelParser(NullLoggerFactory.Instance);

        var source = """
            namespace Tracker.Data.Models
            {
                public partial class TaskUpdateModel
                {
                    [Custom(
                        Name = "Title",
                        IsEnabled = true)]
                    public string Title { get; set; }
                }
            }
            """;

        var result = parser.ParseCode(source);

        Assert.NotNull(result);
        var property = Assert.Single(result.Properties);
        Assert.Equal("Title", property.PropertyName);
        var attribute = Assert.Single(property.Attributes);
        Assert.Contains("[Custom(", attribute);
        Assert.Contains("Name = \"Title\"", attribute);
        Assert.Contains("IsEnabled = true)]", attribute);
    }

    [Fact]
    public void ParseCodeWithoutPropertyAttributesReturnsNull()
    {
        var parser = new ModelParser(NullLoggerFactory.Instance);

        var source = """
            namespace Tracker.Data.Models
            {
                public partial class UserReadModel
                {
                    public string EmailAddress { get; set; }
                }
            }
            """;

        var result = parser.ParseCode(source);

        Assert.Null(result);
    }
}
