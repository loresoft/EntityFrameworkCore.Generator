CREATE OR ALTER FUNCTION [dbo].[FormatAddress]
(
    @AddressLine1 NVARCHAR(255),
    @City NVARCHAR(150),
    @StateProvince NVARCHAR(150),
    @PostalCode NVARCHAR(50)
)
RETURNS NVARCHAR(700)
AS
BEGIN
    RETURN TRIM(
        CONCAT(
            ISNULL(@AddressLine1, ''),
            CASE WHEN @AddressLine1 IS NOT NULL AND @City IS NOT NULL THEN ', ' ELSE '' END,
            ISNULL(@City, ''),
            CASE WHEN @City IS NOT NULL AND @StateProvince IS NOT NULL THEN ', ' ELSE '' END,
            ISNULL(@StateProvince, ''),
            CASE WHEN @StateProvince IS NOT NULL AND @PostalCode IS NOT NULL THEN ' ' ELSE '' END,
            ISNULL(@PostalCode, '')
        )
    );
END
