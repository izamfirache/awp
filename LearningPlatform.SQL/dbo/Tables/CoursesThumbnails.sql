CREATE TABLE [dbo].[CoursesThumbnails]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		[CourseId] INT NULL, 
		[Thumbnail] VARBINARY(MAX) NULL, 
		CONSTRAINT [FK_CoursesThumbnails_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id])
)
