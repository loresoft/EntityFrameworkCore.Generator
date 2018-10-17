using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntityFrameworkCore.Generator.Parsing;
using FluentAssertions;
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
            first.Content.Should().Be("        public Guid Id { get; set; }" + Environment.NewLine);

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
            source.AppendLine(@"    ");
            source.AppendLine(@"        #endregion");
            source.AppendLine(@"    }");
            source.AppendLine(@"}");


            var result = parser.ParseRegions(source.ToString());
            result.Should().NotBeNull();
            result.Count.Should().Be(2);
            
            var nested = result["Nested Properties"];
            nested.Should().NotBeNull();
            nested.Name.Should().Be("Nested Properties");
            nested.Content.Should().Be("        public string EmailAddress { get; set; }" + Environment.NewLine);

            var generated = result["Generated Properties"];
            generated.Should().NotBeNull();
            generated.Name.Should().Be("Generated Properties");

        }


    }
}
