-- Table "Priority" data

INSERT INTO "Priority" 
("Id", "Name", "Description", "DisplayOrder", "IsActive")
VALUES
    ('dbf0e04f-04fb-e811-aa64-1e872cb6cb93', 'High', 'High Priority', 1, True), 
    ('dcf0e04f-04fb-e811-aa64-1e872cb6cb93', 'Normal', 'Normal Priority', 2, True), 
    ('784c7657-04fb-e811-aa64-1e872cb6cb93', 'Low', 'Low Priority', 3, True)
ON CONFLICT ("Id")
DO UPDATE SET 
    "Name" = EXCLUDED."Name", 
    "Description" = EXCLUDED."Description", 
    "DisplayOrder" = EXCLUDED."DisplayOrder", 
    "IsActive" = EXCLUDED."IsActive";

-- Table "Role" data

INSERT INTO "Role" 
("Id", "Name", "Description")
VALUES
    ('b2d78522-0944-e811-bd87-f8633fc30ac7', 'Administrator', 'Administrator'), 
    ('b3d78522-0944-e811-bd87-f8633fc30ac7', 'Manager', 'Manager'), 
    ('acbffa29-0944-e811-bd87-f8633fc30ac7', 'Member', 'Member')
ON CONFLICT ("Id")
DO UPDATE SET 
    "Name" = EXCLUDED."Name", 
    "Description" = EXCLUDED."Description";

-- Table "Status" data

INSERT INTO "Status" 
("Id", "Name", "Description", "DisplayOrder", "IsActive")
VALUES
    ('ce002cd8-04fb-e811-aa64-1e872cb6cb93', 'Not Started', 'Not Starated', 1, True), 
    ('cf002cd8-04fb-e811-aa64-1e872cb6cb93', 'In Progress', 'In Progress', 2, True), 
    ('d0002cd8-04fb-e811-aa64-1e872cb6cb93', 'Completed', 'Completed', 3, True), 
    ('d1002cd8-04fb-e811-aa64-1e872cb6cb93', 'Blocked', 'Blocked', 4, True), 
    ('d2002cd8-04fb-e811-aa64-1e872cb6cb93', 'Deferred', 'Deferred', 5, True), 
    ('d3002cd8-04fb-e811-aa64-1e872cb6cb93', 'Done', 'Done', 6, True)
ON CONFLICT ("Id")
DO UPDATE SET 
    "Name" = EXCLUDED."Name", 
    "Description" = EXCLUDED."Description", 
    "DisplayOrder" = EXCLUDED."DisplayOrder", 
    "IsActive" = EXCLUDED."IsActive";

-- Table "User" data

INSERT INTO "User" 
("Id", "EmailAddress", "IsEmailAddressConfirmed", "DisplayName")
VALUES
    ('83507c95-0744-e811-bd87-f8633fc30ac7', 'william.adama@battlestar.com', True, 'William Adama'), 
    ('490312a6-0744-e811-bd87-f8633fc30ac7', 'laura.roslin@battlestar.com', True, 'Laura Roslin'), 
    ('38da04bb-0744-e811-bd87-f8633fc30ac7', 'kara.thrace@battlestar.com', True, 'Kara Thrace'), 
    ('589d67c6-0744-e811-bd87-f8633fc30ac7', 'lee.adama@battlestar.com', True, 'Lee Adama'), 
    ('118b84d4-0744-e811-bd87-f8633fc30ac7', 'gaius.baltar@battlestar.com', True, 'Gaius Baltar'), 
    ('fa7515df-0744-e811-bd87-f8633fc30ac7', 'saul.tigh@battlestar.com', True, 'Saul Tigh')
ON CONFLICT ("Id")
DO UPDATE SET 
    "EmailAddress" = EXCLUDED."EmailAddress", 
    "IsEmailAddressConfirmed" = EXCLUDED."IsEmailAddressConfirmed", 
    "DisplayName" = EXCLUDED."DisplayName";

