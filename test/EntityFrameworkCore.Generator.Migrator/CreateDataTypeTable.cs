using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010111)]
public class CreateDataTypeTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateDataTypeTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;
    public string DateTimeOffsetType => _providerDefault.DateTimeOffsetType;

    public override void Up()
    {
        Create.Table("DataType")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsInt64()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("Name")
                .AsString(100)
                .NotNullable()

            .WithColumn("Boolean")
                .AsBoolean()
                .NotNullable()

            .WithColumn("Short")
                .AsInt16()
                .NotNullable()

            .WithColumn("Long")
                .AsInt64()
                .NotNullable()

            .WithColumn("Float")
                .AsFloat()
                .NotNullable()

            .WithColumn("Double")
                .AsDouble()
                .NotNullable()

            .WithColumn("Decimal")
                .AsDecimal(19, 4)
                .NotNullable()

            .WithColumn("DateTime")
                .AsDateTime()
                .NotNullable()

            .WithColumn("DateTimeOffset")
                .AsCustom(DateTimeOffsetType)
                .NotNullable()

            .WithColumn("Guid")
                .AsGuid()
                .NotNullable()

            .WithColumn("TimeSpan")
                .AsTime()
                .NotNullable()

            .WithColumn("DateOnly")
                .AsDate()
                .NotNullable()

            .WithColumn("TimeOnly")
                .AsTime()
                .NotNullable()

            .WithColumn("BooleanNull")
                .AsBoolean()
                .Nullable()

            .WithColumn("ShortNull")
                .AsInt16()
                .Nullable()

            .WithColumn("LongNull")
                .AsInt64()
                .Nullable()

            .WithColumn("FloatNull")
                .AsFloat()
                .Nullable()

            .WithColumn("DoubleNull")
                .AsDouble()
                .Nullable()

            .WithColumn("DecimalNull")
                .AsDecimal(19, 4)
                .Nullable()

            .WithColumn("DateTimeNull")
                .AsDateTime()
                .Nullable()

            .WithColumn("DateTimeOffsetNull")
                .AsCustom(DateTimeOffsetType)
                .Nullable()

            .WithColumn("GuidNull")
                .AsGuid()
                .Nullable()

            .WithColumn("TimeSpanNull")
                .AsTime()
                .Nullable()

            .WithColumn("DateOnlyNull")
                .AsDate()
                .Nullable()

            .WithColumn("TimeOnlyNull")
                .AsTime()
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("DataType").InSchema(DefaultSchema);
    }
}
