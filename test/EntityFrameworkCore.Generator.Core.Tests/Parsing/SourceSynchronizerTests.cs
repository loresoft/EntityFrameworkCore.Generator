using EntityFrameworkCore.Generator.Metadata.Generation;
using EntityFrameworkCore.Generator.Options;
using EntityFrameworkCore.Generator.Parsing;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;

namespace EntityFrameworkCore.Generator.Core.Tests.Parsing;

public class SourceSynchronizerTests
{
    [Fact]
    public void UpdateFromSourceResolvesModelDirectoryVariables()
    {
        var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        var modelDirectory = Path.Combine(tempDirectory, "Domain", "User", "Models");
        Directory.CreateDirectory(modelDirectory);

        var modelFile = Path.Combine(modelDirectory, "UserReadModel.cs");
        File.WriteAllText(modelFile, """
            namespace Tracker.Domain.User.Models
            {
                public partial class UserReadModel
                {
                    [Required]
                    public string EmailAddress { get; set; }
                }
            }
            """);

        var options = new GeneratorOptions();
        options.Project.Directory = tempDirectory;
        options.Model.Read.Directory = @"{Project.Directory}\Domain\{Entity.Name}\Models";

        var entity = new Entity
        {
            EntityClass = "User",
            TableSchema = "dbo",
            TableName = "User"
        };
        var model = new Model
        {
            Entity = entity,
            ModelType = ModelType.Read,
            ModelClass = "UserReadModel",
            ModelNamespace = "Tracker.Domain.User.Models"
        };
        model.Properties.Add(new Property
        {
            PropertyName = "EmailAddress",
            ColumnName = "EmailAddress",
            SystemType = typeof(string),
            SystemTypeName = string.Empty,
            IsNullable = false
        });
        entity.Models.Add(model);

        var context = new EntityContext();
        context.Entities.Add(entity);

        var synchronizer = new SourceSynchronizer(NullLoggerFactory.Instance);
        synchronizer.UpdateFromSource(context, options);

        var attributes = Assert.Single(model.PropertyAttributes);
        Assert.Equal("EmailAddress", attributes.Key);
        var attribute = Assert.Single(attributes.Value);
        Assert.Equal("[Required]", attribute);
    }
}
