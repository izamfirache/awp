CREATE TABLE [dbo].[CoursesTags]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		[CourseId] INT NULL, 
		[TagId] INT NULL, 
		CONSTRAINT [FK_CoursesTags_Courses] FOREIGN KEY (CourseId) REFERENCES [Courses]([Id]),
		CONSTRAINT [FK_CoursesTags_Tags] FOREIGN KEY ([TagId]) REFERENCES [Tags]([Id])		
)
