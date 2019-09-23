-- Tables
CREATE TABLE IF NOT EXISTS "Audit" (
    "Id" uuid NOT NULL,
    "Date" timestamp NOT NULL,
    "UserId" uuid NULL,
    "TaskId" uuid NULL,
    "Content" text NOT NULL,
    "Username" varchar(50) NOT NULL,
    "Created" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "CreatedBy" varchar(100) NULL,
    "Updated" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "UpdatedBy" varchar(100) NULL,
    "RowVersion" bytea NULL,
    CONSTRAINT "PK_Audit" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Priority" (
    "Id" uuid NOT NULL,
    "Name" varchar(100) NOT NULL,
    "Description" varchar(255) NULL,
    "DisplayOrder" int4 NOT NULL DEFAULT (0),
    "IsActive" bool NOT NULL DEFAULT (true),
    "Created" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "CreatedBy" varchar(100) NULL,
    "Updated" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "UpdatedBy" varchar(100) NULL,
    "RowVersion" bytea NULL,
    CONSTRAINT "PK_Priority" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Role" (
    "Id" uuid NOT NULL,
    "Name" varchar(256) NOT NULL,
    "Description" text NULL,
    "Created" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "CreatedBy" varchar(100) NULL,
    "Updated" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "UpdatedBy" varchar(100) NULL,
    "RowVersion" bytea NULL,
    CONSTRAINT "PK_Role" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Status" (
    "Id" uuid NOT NULL,
    "Name" varchar(100) NOT NULL,
    "Description" varchar(255) NULL,
    "DisplayOrder" int4 NOT NULL DEFAULT (0),
    "IsActive" bool NOT NULL DEFAULT (true),
    "Created" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "CreatedBy" varchar(100) NULL,
    "Updated" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "UpdatedBy" varchar(100) NULL,
    "RowVersion" bytea NULL,
    CONSTRAINT "PK_Status" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "Task" (
    "Id" uuid NOT NULL,
    "StatusId" uuid NOT NULL,
    "PriorityId" uuid NULL,
    "Title" varchar(255) NOT NULL,
    "Description" text NULL,
    "StartDate" timestamptz NULL,
    "DueDate" timestamptz NULL,
    "CompleteDate" timestamptz NULL,
    "AssignedId" uuid NULL,
    "Created" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "CreatedBy" varchar(100) NULL,
    "Updated" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "UpdatedBy" varchar(100) NULL,
    "RowVersion" bytea NULL,
    CONSTRAINT "PK_Task" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "TaskExtended" (
    "TaskId" uuid NOT NULL,
    "UserAgent" text NULL,
    "Browser" varchar(256) NULL,
    "OperatingSystem" varchar(256) NULL,
    "Created" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "CreatedBy" varchar(100) NULL,
    "Updated" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "UpdatedBy" varchar(100) NULL,
    "RowVersion" bytea NULL,
    CONSTRAINT "PK_TaskExtended" PRIMARY KEY ("TaskId")
);

CREATE TABLE IF NOT EXISTS "User" (
    "Id" uuid NOT NULL,
    "EmailAddress" varchar(256) NOT NULL,
    "IsEmailAddressConfirmed" bool NOT NULL DEFAULT (false),
    "DisplayName" varchar(256) NOT NULL,
    "PasswordHash" text NULL,
    "ResetHash" text NULL,
    "InviteHash" text NULL,
    "AccessFailedCount" int4 NOT NULL DEFAULT (0),
    "LockoutEnabled" bool NOT NULL DEFAULT (false),
    "LockoutEnd" timestamptz NULL,
    "LastLogin" timestamptz NULL,
    "IsDeleted" bool NOT NULL DEFAULT (false),
    "Created" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "CreatedBy" varchar(100) NULL,
    "Updated" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "UpdatedBy" varchar(100) NULL,
    "RowVersion" bytea NULL,
    CONSTRAINT "PK_User" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "UserLogin" (
    "Id" uuid NOT NULL,
    "EmailAddress" varchar(256) NOT NULL,
    "UserId" uuid NULL,
    "UserAgent" text NULL,
    "Browser" varchar(256) NULL,
    "OperatingSystem" varchar(256) NULL,
    "DeviceFamily" varchar(256) NULL,
    "DeviceBrand" varchar(256) NULL,
    "DeviceModel" varchar(256) NULL,
    "IpAddress" varchar(50) NULL,
    "IsSuccessful" bool NOT NULL DEFAULT (false),
    "FailureMessage" varchar(256) NULL,
    "Created" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "CreatedBy" varchar(100) NULL,
    "Updated" timestamptz NOT NULL DEFAULT (now() at time zone 'utc'),
    "UpdatedBy" varchar(100) NULL,
    "RowVersion" bytea NULL,
    CONSTRAINT "PK_UserLogin" PRIMARY KEY ("Id")
);

CREATE TABLE IF NOT EXISTS "UserRole" (
    "UserId" uuid NOT NULL,
    "RoleId" uuid NOT NULL,
    CONSTRAINT "PK_UserRole" PRIMARY KEY ("UserId", "RoleId")
);


-- Foreign Keys
ALTER TABLE IF EXISTS "Task"
    ADD CONSTRAINT "FK_Task_Priority_PriorityId" FOREIGN KEY ("PriorityId") REFERENCES "Priority" ("Id");

ALTER TABLE IF EXISTS "Task"
    ADD CONSTRAINT "FK_Task_Status_StatusId" FOREIGN KEY ("StatusId") REFERENCES "Status" ("Id");

ALTER TABLE IF EXISTS "Task"
    ADD CONSTRAINT "FK_Task_User_AssignedId" FOREIGN KEY ("AssignedId") REFERENCES "User" ("Id");

ALTER TABLE IF EXISTS "TaskExtended"
    ADD CONSTRAINT "FK_TaskExtended_Task_TaskId" FOREIGN KEY ("TaskId") REFERENCES "Task" ("Id");

ALTER TABLE IF EXISTS "UserLogin"
    ADD CONSTRAINT "FK_UserLogin_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id");

ALTER TABLE IF EXISTS "UserRole"
    ADD CONSTRAINT "FK_UserRole_Role_RoleId" FOREIGN KEY ("RoleId") REFERENCES "Role" ("Id");

ALTER TABLE IF EXISTS "UserRole"
    ADD CONSTRAINT "FK_UserRole_User_UserId" FOREIGN KEY ("UserId") REFERENCES "User" ("Id");


-- Indexes
CREATE UNIQUE INDEX IF NOT EXISTS "UX_Role_Name"
ON "Role" ("Name");

CREATE INDEX IF NOT EXISTS "IX_Task_AssignedId"
ON "Task" ("AssignedId");

CREATE INDEX IF NOT EXISTS "IX_Task_PriorityId"
ON "Task" ("PriorityId");

CREATE INDEX IF NOT EXISTS "IX_Task_StatusId"
ON "Task" ("StatusId");

CREATE UNIQUE INDEX IF NOT EXISTS "UX_User_EmailAddress"
ON "User" ("EmailAddress");

CREATE INDEX IF NOT EXISTS "IX_UserLogin_EmailAddress"
ON "UserLogin" ("EmailAddress");

CREATE INDEX IF NOT EXISTS "IX_UserLogin_UserId"
ON "UserLogin" ("UserId");


