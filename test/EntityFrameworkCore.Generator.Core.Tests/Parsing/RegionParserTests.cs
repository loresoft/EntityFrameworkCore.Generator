using EntityFrameworkCore.Generator.Parsing;
using FluentAssertions;
using System;
using System.Linq;
using System.Text;
using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests.Parsing
{
    public class RegionParserTests
    {
        [Fact]
        public void ParseRegions()
        {
            var parser = new RegionParser();

            var source = new StringBuilder();
            source.AppendLine(@"using System;");
            source.AppendLine(@"using System.Collections.Generic;");
            source.AppendLine(@"");
            source.AppendLine(@"namespace EntityFrameworkCore.Generator.Core.Tests");
            source.AppendLine(@"{");
            source.AppendLine(@"    public partial class User");
            source.AppendLine(@"    {");
            source.AppendLine(@"        #region Generated Properties");
            source.AppendLine(@"        public Guid Id { get; set; }");
            source.AppendLine(@"        #endregion");
            source.AppendLine(@"    }");
            source.AppendLine(@"}");


            var result = parser.ParseRegions(source.ToString());
            result.Should().NotBeNull();
            result.Count.Should().Be(1);

            var first = result.Values.First();
            first.Should().NotBeNull();
            first.Name.Should().Be("Generated Properties");

            var content = new StringBuilder();
            content.AppendLine(@"#region Generated Properties");
            content.AppendLine(@"        public Guid Id { get; set; }");
            content.AppendLine(@"        #endregion");

            first.Content.Should().Be(content.ToString());

        }

        [Fact]
        public void ParseMultipleRegions()
        {
            var parser = new RegionParser();

            var source = new StringBuilder();
            source.AppendLine(@"using System;");
            source.AppendLine(@"using System.Collections.Generic;");
            source.AppendLine(@"");
            source.AppendLine(@"namespace EntityFrameworkCore.Generator.Core.Tests");
            source.AppendLine(@"{");
            source.AppendLine(@"    public partial class User");
            source.AppendLine(@"    {");
            source.AppendLine(@"        public User()");
            source.AppendLine(@"        {");
            source.AppendLine(@"            #region Generated Initializes");
            source.AppendLine(@"            Created = DateTimeOffset.UtcNow;");
            source.AppendLine(@"            Updated = DateTimeOffset.UtcNow;");
            source.AppendLine(@"            UserRoles = new HashSet<UserRole>();");
            source.AppendLine(@"            #endregion");
            source.AppendLine(@"        }");
            source.AppendLine(@"");
            source.AppendLine(@"        #region Generated Properties");
            source.AppendLine(@"        public Guid Id { get; set; }");
            source.AppendLine(@"        public string EmailAddress { get; set; }");
            source.AppendLine(@"        public DateTimeOffset Created { get; set; }");
            source.AppendLine(@"        public DateTimeOffset Updated { get; set; }");
            source.AppendLine(@"        #endregion");
            source.AppendLine(@"");
            source.AppendLine(@"        #region Generated Relationships");
            source.AppendLine(@"        public virtual ICollection<UserRole> UserRoles { get; set; }");
            source.AppendLine(@"        #endregion");
            source.AppendLine(@"    }");
            source.AppendLine(@"}");


            var result = parser.ParseRegions(source.ToString());
            result.Should().NotBeNull();
            result.Count.Should().Be(3);

            var first = result.Values.First();
            first.Should().NotBeNull();
            first.Name.Should().Be("Generated Initializes");
        }

        [Fact]
        public void ParseNestedRegions()
        {
            var parser = new RegionParser();

            var source = new StringBuilder();
            source.AppendLine(@"using System;");
            source.AppendLine(@"using System.Collections.Generic;");
            source.AppendLine(@"");
            source.AppendLine(@"namespace EntityFrameworkCore.Generator.Core.Tests");
            source.AppendLine(@"{");
            source.AppendLine(@"    public partial class User");
            source.AppendLine(@"    {");
            source.AppendLine(@"        #region Generated Properties");
            source.AppendLine(@"        public Guid Id { get; set; }");
            source.AppendLine(@"");
            source.AppendLine(@"        #region Nested Properties");
            source.AppendLine(@"        public string EmailAddress { get; set; }");
            source.AppendLine(@"        #endregion");
            source.AppendLine(@"");
            source.AppendLine(@"        #endregion");
            source.AppendLine(@"    }");
            source.AppendLine(@"}");


            var result = parser.ParseRegions(source.ToString());
            result.Should().NotBeNull();
            result.Count.Should().Be(2);

            var nested = result["Nested Properties"];
            nested.Should().NotBeNull();
            nested.Name.Should().Be("Nested Properties");

            var nestedContent = new StringBuilder();
            nestedContent.AppendLine(@"#region Nested Properties");
            nestedContent.AppendLine(@"        public string EmailAddress { get; set; }");
            nestedContent.AppendLine(@"        #endregion");

            nested.Content.Should().Be(nestedContent.ToString());

            var generated = result["Generated Properties"];
            generated.Should().NotBeNull();
            generated.Name.Should().Be("Generated Properties");

            var generatedContent = new StringBuilder();
            generatedContent.AppendLine(@"#region Generated Properties");
            generatedContent.AppendLine(@"        public Guid Id { get; set; }");
            generatedContent.AppendLine(@"");
            generatedContent.AppendLine(@"        #region Nested Properties");
            generatedContent.AppendLine(@"        public string EmailAddress { get; set; }");
            generatedContent.AppendLine(@"        #endregion");
            generatedContent.AppendLine(@"");
            generatedContent.AppendLine(@"        #endregion");

            generated.Content.Should().Be(generatedContent.ToString());

        }


    }
}
