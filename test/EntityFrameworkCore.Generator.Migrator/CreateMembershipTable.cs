using EntityFrameworkCore.Generator.Migrator.Providers;

using FluentMigrator;

namespace EntityFrameworkCore.Generator.Migrator;

[Migration(2026010211)]
public class CreateMembershipTable : Migration
{
    private readonly IProviderDefault _providerDefault;

    public CreateMembershipTable(IProviderDefault providerDefault)
    {
        _providerDefault = providerDefault;
    }

    public string DefaultSchema => _providerDefault.DefaultSchema;

    public override void Up()
    {
        Create.Table("Membership")
            .InSchema(DefaultSchema)

            .WithColumn("UserId")
                .AsGuid()
                .NotNullable()
                .PrimaryKey()

            .WithColumn("UserName")
                .AsString(256)
                .NotNullable()

            .WithColumn("LoweredUserName")
                .AsString(256)
                .NotNullable()

            .WithColumn("MobileAlias")
                .AsString(16)
                .Nullable()

            .WithColumn("Password")
                .AsString(128)
                .NotNullable()

            .WithColumn("PasswordFormat")
                .AsInt32()
                .NotNullable()
                .WithDefaultValue(0)

            .WithColumn("PasswordSalt")
                .AsString(128)
                .NotNullable()

            .WithColumn("MobilePIN")
                .AsString(16)
                .Nullable()

            .WithColumn("Email")
                .AsString(256)
                .NotNullable()

            .WithColumn("LoweredEmail")
                .AsString(256)
                .NotNullable()

            .WithColumn("PasswordQuestion")
                .AsString(256)
                .Nullable()

            .WithColumn("PasswordAnswer")
                .AsString(128)
                .Nullable()

            .WithColumn("IsApproved")
                .AsBoolean()
                .NotNullable()

            .WithColumn("IsLockedOut")
                .AsBoolean()
                .NotNullable()

            .WithColumn("IsAnonymous")
                .AsBoolean()
                .NotNullable()
                .WithDefaultValue(false)

            .WithColumn("CreateDate")
                .AsDateTime()
                .NotNullable()

            .WithColumn("LastLoginDate")
                .AsDateTime()
                .NotNullable()

            .WithColumn("LastActivityDate")
                .AsDateTime()
                .NotNullable()

            .WithColumn("LastPasswordChangedDate")
                .AsDateTime()
                .NotNullable()

            .WithColumn("LastLockoutDate")
                .AsDateTime()
                .NotNullable()

            .WithColumn("FailedPasswordAttemptCount")
                .AsInt32()
                .NotNullable()

            .WithColumn("FailedPasswordAttemptWindowStart")
                .AsDateTime()
                .NotNullable()

            .WithColumn("FailedPasswordAnswerAttemptCount")
                .AsInt32()
                .NotNullable()

            .WithColumn("FailedPasswordAnswerAttemptWindowStart")
                .AsDateTime()
                .NotNullable()

            .WithColumn("Comment")
                .AsString()
                .Nullable();

        Create.Index("IX_Password")
            .OnTable("Membership")
            .InSchema(DefaultSchema)
            .OnColumn("LoweredUserName").Ascending()
            .OnColumn("Password").Ascending()
            .OnColumn("IsApproved").Ascending()
            .OnColumn("IsLockedOut").Ascending();
    }

    public override void Down()
    {
        Delete.Table("Membership").InSchema(DefaultSchema);
    }
}
