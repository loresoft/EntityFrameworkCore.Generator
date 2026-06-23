CREATE OR REPLACE PROCEDURE public."StatusPaged"(
    IN "Offset" integer,
    IN "Limit" integer,
    OUT "Total" bigint
)
LANGUAGE plpgsql
AS $$
BEGIN
    SELECT COUNT(*) INTO "Total"
    FROM public."Status";
END;
$$;
