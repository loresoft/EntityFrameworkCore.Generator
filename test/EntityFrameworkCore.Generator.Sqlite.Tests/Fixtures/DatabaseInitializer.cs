using FluentMigrator.Runner;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EntityFrameworkCore.Generator.Sqlite.Tests.Fixtures;

public class DatabaseInitializer : IHostedService
{
    private readonly ILogger<DatabaseInitializer> _logger;
    private readonly IMigrationRunner _migrationRunner;

    public DatabaseInitializer(
        ILogger<DatabaseInitializer> logger,
        IMigrationRunner migrationRunner)
    {
        _logger = logger;
        _migrationRunner = migrationRunner;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Starting database migration for {DatabaseType}...",
            _migrationRunner.Processor.DatabaseType);

        // Enable foreign key constraints
        _migrationRunner.Processor.Execute("PRAGMA foreign_keys = ON");

        // run migrations
        _migrationRunner.MigrateUp();

        CreateSqliteForeignKeys();
        CreateSqliteEdgeCaseObjects();

        return Task.CompletedTask;
    }

    private void CreateSqliteForeignKeys()
    {
        RecreateTaskTable();
        RecreateTaskExtendedTable();
        RecreateUserRoleTable();
    }

    private void RecreateTaskExtendedTable()
    {
        _migrationRunner.Processor.Execute("""
            ALTER TABLE "TaskExtended"
            RENAME TO "TaskExtended_Migration"
            """);

        _migrationRunner.Processor.Execute("""
            CREATE TABLE "TaskExtended" (
                "TaskId" UNIQUEIDENTIFIER NOT NULL,
                "UserAgent" TEXT,
                "Browser" NVARCHAR(256),
                "OperatingSystem" NVARCHAR(256),
                "Created" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                "CreatedBy" NVARCHAR(100),
                "Updated" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                "UpdatedBy" NVARCHAR(100),
                "RowVersion" INTEGER NOT NULL,
                CONSTRAINT "PK_TaskExtended" PRIMARY KEY ("TaskId"),
                CONSTRAINT "FK_TaskExtended_Task_TaskId" FOREIGN KEY ("TaskId") REFERENCES "Task" ("Id")
            )
            """);

        _migrationRunner.Processor.Execute("""
            INSERT INTO "TaskExtended" (
                "TaskId",
                "UserAgent",
                "Browser",
                "OperatingSystem",
                "Created",
                "CreatedBy",
                "Updated",
                "UpdatedBy",
                "RowVersion"
            )
            SELECT
                "TaskId",
                "UserAgent",
                "Browser",
                "OperatingSystem",
                "Created",
                "CreatedBy",
                "Updated",
                "UpdatedBy",
                "RowVersion"
            FROM "TaskExtended_Migration"
            """);

        _migrationRunner.Processor.Execute("""
            DROP TABLE "TaskExtended_Migration"
            """);
    }

    private void RecreateTaskTable()
    {
        _migrationRunner.Processor.Execute("""
            ALTER TABLE "Task"
            RENAME TO "Task_Migration"
            """);

        _migrationRunner.Processor.Execute("""
            CREATE TABLE "Task" (
                "Id" UNIQUEIDENTIFIER NOT NULL,
                "StatusId" INTEGER NOT NULL,
                "PriorityId" INTEGER,
                "Title" NVARCHAR(255) NOT NULL,
                "Description" TEXT,
                "StartDate" DATETIME,
                "DueDate" DATETIME,
                "CompleteDate" DATETIME,
                "AssignedId" INTEGER,
                "Created" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                "CreatedBy" NVARCHAR(100),
                "Updated" DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                "UpdatedBy" NVARCHAR(100),
                "RowVersion" INTEGER NOT NULL,
                CONSTRAINT "PK_Task" PRIMARY KEY ("Id"),
                CONSTRAINT "FK_Task_Priority_PriorityId" FOREIGN KEY ("PriorityId") REFERENCES "Priority" ("Id"),
                CONSTRAINT "FK_Task_Status_StatusId" FOREIGN KEY ("StatusId") REFERENCES "Status" ("Id"),
                CONSTRAINT "FK_Task_User_AssignedId" FOREIGN KEY ("AssignedId") REFERENCES "User" ("Id")
            )
            """);

        _migrationRunner.Processor.Execute("""
            INSERT INTO "Task" (
                "Id",
                "StatusId",
                "PriorityId",
                "Title",
                "Description",
                "StartDate",
                "DueDate",
                "CompleteDate",
                "AssignedId",
                "Created",
                "CreatedBy",
                "Updated",
                "UpdatedBy",
                "RowVersion"
            )
            SELECT
                "Id",
                "StatusId",
                "PriorityId",
                "Title",
                "Description",
                "StartDate",
                "DueDate",
                "CompleteDate",
                "AssignedId",
                "Created",
                "CreatedBy",
                "Updated",
                "UpdatedBy",
                "RowVersion"
            FROM "Task_Migration"
            """);

        _migrationRunner.Processor.Execute("""
            DROP TABLE "Task_Migration"
            """);

        _migrationRunner.Processor.Execute("""
            CREATE INDEX "IX_Task_AssignedId"
            ON "Task" ("AssignedId")
            """);

        _migrationRunner.Processor.Execute("""
            CREATE INDEX "IX_Task_PriorityId"
            ON "Task" ("PriorityId")
            """);

        _migrationRunner.Processor.Execute("""
            CREATE INDEX "IX_Task_StatusId"
            ON "Task" ("StatusId")
            """);
    }

    private void RecreateUserRoleTable()
    {
        _migrationRunner.Processor.Execute("""
            ALTER TABLE "UserRole"
            RENAME TO "UserRole_Migration"
            """);

        _migrationRunner.Processor.Execute("""
            CREATE TABLE "UserRole" (
                "UserId" INTEGER NOT NULL,
                "RoleId" INTEGER NOT NULL,
                CONSTRAINT "PK_UserRole" PRIMARY KEY ("UserId", "RoleId"),
                CONSTRAINT "FK_UserRole_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Role" ("Id"),
                CONSTRAINT "FK_UserRole_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id")
            )
            """);

        _migrationRunner.Processor.Execute("""
            INSERT INTO "UserRole" ("UserId", "RoleId")
            SELECT "UserId", "RoleId"
            FROM "UserRole_Migration"
            """);

        _migrationRunner.Processor.Execute("""
            DROP TABLE "UserRole_Migration"
            """);
    }

    private void CreateSqliteEdgeCaseObjects()
    {
        _migrationRunner.Processor.Execute("""
            CREATE TABLE IF NOT EXISTS "Computed Column" (
                "Id" INTEGER PRIMARY KEY,
                "First Name" TEXT NOT NULL,
                "Last Name" TEXT NOT NULL,
                "Full Name" TEXT GENERATED ALWAYS AS ("First Name" || ' ' || "Last Name") VIRTUAL,
                "Search Name" TEXT GENERATED ALWAYS AS (lower("First Name" || ' ' || "Last Name")) STORED
            )
            """);

        _migrationRunner.Processor.Execute("""
            CREATE INDEX IF NOT EXISTS "IX_Computed Column_Search Name_Active"
            ON "Computed Column" ("Search Name")
            WHERE "Last Name" <> ''
            """);

        _migrationRunner.Processor.Execute("""
            CREATE INDEX IF NOT EXISTS "IX_Computed Column_Lower_First Name"
            ON "Computed Column" (lower("First Name"))
            """);

        _migrationRunner.Processor.Execute("""
            CREATE VIEW IF NOT EXISTS "Active Users" AS
            SELECT "Id", "UserName", "EmailAddress"
            FROM "User"
            WHERE "IsDeleted" = 0
            """);
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
