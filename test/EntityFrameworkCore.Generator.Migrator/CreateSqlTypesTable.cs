using EntityFrameworkCore.Generator.Migrator.Extensions;
using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010205)]
public class CreateSqlTypesTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateSqlTypesTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public string RowVersionType => _providerDefault.RowVersionType;
    public bool SupportIdentity => _providerDefault.SupportIdentity;

    public override void Up()
    {
        Create.Table("SqlTypes")
            .InSchema(DefaultSchema)

            .WithColumn("Id")
                .AsInt32()
                .IdentityIf(SupportIdentity)
                .NotNullable()
                .PrimaryKey()

            .WithColumn("BigInt")
                .AsInt64()
                .Nullable()

            .WithColumn("Binary")
                .AsBinary(50)
                .Nullable()

            .WithColumn("Bit")
                .AsBoolean()
                .Nullable()

            .WithColumn("Char")
                .AsFixedLengthAnsiString(10)
                .Nullable()

            .WithColumn("DateTime")
                .AsDateTime()
                .Nullable()

            .WithColumn("Decimal")
                .AsDecimal(18, 0)
                .Nullable()

            .WithColumn("Float")
                .AsDouble()
                .Nullable()

            .WithColumn("Image")
                .AsBinary(int.MaxValue)
                .Nullable()

            .WithColumn("Int")
                .AsInt32()
                .Nullable()

            .WithColumn("Money")
                .AsCurrency()
                .Nullable()

            .WithColumn("NChar")
                .AsFixedLengthString(10)
                .Nullable()

            .WithColumn("NText")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("Numeric")
                .AsDecimal(18, 0)
                .Nullable()

            .WithColumn("NVarChar")
                .AsString(50)
                .Nullable()

            .WithColumn("NVarCharMax")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("Real")
                .AsFloat()
                .Nullable()

            .WithColumn("SmallDateTime")
                .AsDateTime()
                .Nullable()

            .WithColumn("SmallInt")
                .AsInt16()
                .Nullable()

            .WithColumn("SmallMoney")
                .AsCurrency()
                .Nullable()

            .WithColumn("Text")
                .AsString(int.MaxValue)
                .Nullable()

            .WithColumn("TimeStamp")
                .AsCustom(RowVersionType)
                .Nullable()

            .WithColumn("TinyInt")
                .AsByte()
                .Nullable()

            .WithColumn("UniqueIdentifier")
                .AsGuid()
                .Nullable()

            .WithColumn("VarBinary")
                .AsBinary(50)
                .Nullable()

            .WithColumn("VarBinaryMax")
                .AsBinary(int.MaxValue)
                .Nullable()

            .WithColumn("VarChar")
                .AsAnsiString(50)
                .Nullable()

            .WithColumn("VarCharMax")
                .AsAnsiString(int.MaxValue)
                .Nullable()

            .WithColumn("Xml")
                .AsString(int.MaxValue)
                .Nullable();
    }

    public override void Down()
    {
        Delete.Table("SqlTypes").InSchema(DefaultSchema);
    }
}
