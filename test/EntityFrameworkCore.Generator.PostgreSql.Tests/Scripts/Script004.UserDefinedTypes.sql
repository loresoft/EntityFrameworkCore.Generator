CREATE TYPE public."TaskState" AS ENUM ('New', 'Active', 'Closed');

CREATE DOMAIN public."EmailAddress" AS character varying(256);

CREATE TYPE public."IdentifierPair" AS (
    "Id" integer,
    "Name" text
);
