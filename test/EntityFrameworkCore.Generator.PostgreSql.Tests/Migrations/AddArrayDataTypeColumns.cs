using FluentMigrator;

namespace EntityFrameworkCore.Generator.PostgreSql.Tests.Migrations;

[Migration(2026020104)]
public class AddArrayDataTypeColumns : Migration
{
    public override void Up()
    {
        Alter.Table("DataType")
            .InSchema("public")
            .AddColumn("IntegerArray")
                .AsCustom("integer[]")
                .NotNullable()
            .AddColumn("TextArray")
                .AsCustom("text[]")
                .NotNullable()
            .AddColumn("NullableVarcharArray")
                .AsCustom("character varying(50)[]")
                .Nullable();
    }

    public override void Down()
    {
        Delete.Column("IntegerArray")
            .Column("TextArray")
            .Column("NullableVarcharArray")
            .FromTable("DataType")
            .InSchema("public");
    }
}
