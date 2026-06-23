CREATE INDEX "IX_Task_TitleLower" ON public."Task" (lower("Title"));

CREATE INDEX "IX_Task_Created_Desc" ON public."Task" ("Created" DESC);

CREATE OR REPLACE FUNCTION public."NormalizeEmailAddress"(
    "EmailAddress" public."EmailAddress"
)
RETURNS public."EmailAddress"
LANGUAGE sql
AS $$
    SELECT lower("EmailAddress")::public."EmailAddress";
$$;

COMMENT ON PROCEDURE public."StatusPaged"(integer, integer) IS 'Reads a page of statuses.';
COMMENT ON FUNCTION public."FormatAddress"(text, text, text, text) IS 'Formats an address.';
COMMENT ON FUNCTION public."NormalizeEmailAddress"(public."EmailAddress") IS 'Normalizes an email address.';
