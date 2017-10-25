CREATE TABLE [dbo].[Courses]
(
	[Id] INT NOT NULL PRIMARY KEY, 
		[Name] NVARCHAR(MAX) NULL, 
		[Description] NVARCHAR(MAX) NULL, 
		[CreationDate] DATETIME NULL, 
		[UpdateDate] DATETIME NULL
)
