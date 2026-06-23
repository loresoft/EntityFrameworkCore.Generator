CREATE OR REPLACE VIEW public."PriorityDropdown"
AS
SELECT "Id", "Name", "DisplayOrder"
FROM public."Priority";

CREATE OR REPLACE VIEW public."StatusDropdown"
AS
SELECT "Id", "Name", "DisplayOrder"
FROM public."Status";
