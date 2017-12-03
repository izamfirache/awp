CREATE TABLE [dbo].[Courses]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		[Name] NVARCHAR(MAX) NULL, 
		[Description] NVARCHAR(MAX) NULL, 
		[CreationDate] DATETIME NULL, 
		[UpdateDate] DATETIME NULL, 
		[IsFeatured] INT NULL,
		[ContentHtml] NVARCHAR(MAX) NULL
)
