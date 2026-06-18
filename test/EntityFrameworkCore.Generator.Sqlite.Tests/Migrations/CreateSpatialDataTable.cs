using FluentMigrator;

namespace EntityFrameworkCore.Generator.Sqlite.Tests.Migrations;

[Migration(2026020104)]
public class CreateSpatialDataTable : Migration
{
    public override void Up()
    {
        Create.Table("SpatialData")
            .WithColumn("Id")
                .AsInt32()
                .NotNullable()
                .PrimaryKey()
            .WithColumn("GeometryValue")
                .AsCustom("GEOMETRY")
                .Nullable()
            .WithColumn("PointValue")
                .AsCustom("POINT")
                .Nullable()
            .WithColumn("GeographyValue")
                .AsCustom("GEOGRAPHY")
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("SpatialData");
    }
}
