﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY, 
		[Username] NVARCHAR(MAX) NULL, 
		[Password] NVARCHAR(MAX) NULL, 
		[Email] NVARCHAR(MAX) NULL
)
