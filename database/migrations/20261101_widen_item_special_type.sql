-- QuickBooks SpecialItemType values in the restored company exceed 24 characters.
-- Preserve the complete source value; this migration only expands varchar columns.
IF EXISTS (SELECT 1 FROM sys.columns c JOIN sys.types t ON t.user_type_id=c.user_type_id
           WHERE c.object_id=OBJECT_ID('dbo.Item') AND c.name='SpecialItemType'
             AND t.name='varchar' AND c.max_length BETWEEN 1 AND 63)
    ALTER TABLE dbo.Item ALTER COLUMN SpecialItemType varchar(64) NULL;