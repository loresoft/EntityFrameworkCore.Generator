CREATE OR REPLACE FUNCTION public."FormatAddress"(
    "AddressLine1" text,
    "City" text,
    "StateProvince" text,
    "PostalCode" text
)
RETURNS text
LANGUAGE sql
AS $$
    SELECT concat_ws(', ', "AddressLine1", "City", "StateProvince", "PostalCode");
$$;
