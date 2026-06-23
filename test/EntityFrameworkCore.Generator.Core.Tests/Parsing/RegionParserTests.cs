using System.Linq;

using EntityFrameworkCore.Generator.Parsing;

using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests.Parsing;

public class RegionParserTests
{
    [Fact]
    public void ParseRegions()
    {
        var source = """
            using System;
            using System.Collections.Generic;

            namespace EntityFrameworkCore.Generator.Core.Tests
            {
                public partial class User
                {
                    #region Generated Properties
                    public Guid Id { get; set; }
                    #endregion
                }
            }
            """;


        var result = RegionParser.ParseRegions(source);
        Assert.NotNull(result);
        Assert.Single(result);

        var first = result.First();
        Assert.NotNull(first);
        Assert.Equal("Generated Properties", first.RegionName);

        var content = """
            #region Generated Properties
                    public Guid Id { get; set; }
                    #endregion
            """ + Environment.NewLine;

        Assert.Equal(content, first.Content);

    }

    [Fact]
    public void ParseMultipleRegions()
    {
        var source = """
            using System;
            using System.Collections.Generic;

            namespace EntityFrameworkCore.Generator.Core.Tests
            {
                public partial class User
                {
                    public User()
                    {
                        #region Generated Initializes
                        Created = DateTimeOffset.UtcNow;
                        Updated = DateTimeOffset.UtcNow;
                        UserRoles = new HashSet<UserRole>();
                        #endregion
                    }

                    #region Generated Properties
                    public Guid Id { get; set; }
                    public string EmailAddress { get; set; }
                    public DateTimeOffset Created { get; set; }
                    public DateTimeOffset Updated { get; set; }
                    #endregion

                    #region Generated Relationships
                    public virtual ICollection<UserRole> UserRoles { get; set; }
                    #endregion
                }
            }
            """;


        var result = RegionParser.ParseRegions(source);
        Assert.NotNull(result);
        Assert.Equal(3, result.Count);

        var first = result.First();
        Assert.NotNull(first);
        Assert.Equal("Generated Initializes", first.RegionName);
    }

    [Fact]
    public void ParseNestedRegions()
    {
        var source = """
            using System;
            using System.Collections.Generic;

            namespace EntityFrameworkCore.Generator.Core.Tests
            {
                public partial class User
                {
                    #region Generated Properties
                    public Guid Id { get; set; }

                    #region Nested Properties
                    public string EmailAddress { get; set; }
                    #endregion

                    #endregion
                }
            }
            """;


        var result = RegionParser.ParseRegions(source);
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        var nested = result.Find(p => p.RegionName == "Nested Properties");
        Assert.NotNull(nested);
        Assert.Equal("Nested Properties", nested.RegionName);

        var nestedContent = """
            #region Nested Properties
                    public string EmailAddress { get; set; }
                    #endregion
            """ + Environment.NewLine;

        Assert.Equal(nestedContent, nested.Content);

        var generated = result.Find(p => p.RegionName == "Generated Properties");
        Assert.NotNull(generated);
        Assert.Equal("Generated Properties", generated.RegionName);

        var generatedContent = """
            #region Generated Properties
                    public Guid Id { get; set; }

                    #region Nested Properties
                    public string EmailAddress { get; set; }
                    #endregion

                    #endregion
            """ + Environment.NewLine;

        Assert.Equal(generatedContent, generated.Content);

    }

    [Fact]
    public void ParseRegionsMultipleClasses()
    {
        var source = """
            using System;
            using System.Collections.Generic;

            namespace EntityFrameworkCore.Generator.Core.Tests;
            public partial class User
            {
                #region Generated Properties
                public Guid UserId { get; set; }
                #endregion
            }
            public partial class Tester
            {
                #region Generated Properties
                public Guid TesterId { get; set; }
                #endregion
            }
            """;


        var result = RegionParser.ParseRegions(source);
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        var first = result[0];
        Assert.NotNull(first);
        Assert.Equal("Generated Properties", first.RegionName);
        Assert.Equal("User", first.ClassName);

        var firstContent = """
            #region Generated Properties
                public Guid UserId { get; set; }
                #endregion
            """ + Environment.NewLine;

        Assert.Equal(firstContent, first.Content);

        var second = result[1];
        Assert.NotNull(second);
        Assert.Equal("Generated Properties", second.RegionName);
        Assert.Equal("Tester", second.ClassName);

        var secondContent = """
            #region Generated Properties
                public Guid TesterId { get; set; }
                #endregion
            """ + Environment.NewLine;

        Assert.Equal(secondContent, second.Content);
    }

}
