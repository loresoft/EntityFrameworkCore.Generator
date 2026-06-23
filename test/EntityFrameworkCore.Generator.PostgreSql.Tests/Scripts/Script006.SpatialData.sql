CREATE EXTENSION IF NOT EXISTS postgis;

CREATE TABLE IF NOT EXISTS "SpatialData"
(
    "Id" integer NOT NULL PRIMARY KEY,
    "GeometryValue" geometry NULL,
    "GeographyValue" geography NULL
);
