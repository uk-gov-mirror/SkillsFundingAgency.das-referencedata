﻿IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'charity' AND COLUMN_NAME='name' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [CharityData].[charity] ADD DEFAULT '' FOR [name]
END

IF EXISTS(SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'charity' AND COLUMN_NAME='nhs' AND IS_NULLABLE = 'YES')
BEGIN
	ALTER TABLE [CharityData].[charity] ADD DEFAULT '' FOR [nhs]
END