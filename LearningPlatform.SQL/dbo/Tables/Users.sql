﻿CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		[Username] NVARCHAR(MAX) NULL, 
		[Password] NVARCHAR(MAX) NULL, 
		[Email] NVARCHAR(MAX) NULL, 
		[Avatar] VARBINARY(MAX) NULL, 
    [AvatarHex] NCHAR(10) NULL
)
