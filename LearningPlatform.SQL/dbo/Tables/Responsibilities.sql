CREATE TABLE [dbo].[Responsibilities]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
		[UserId] INT NOT NULL, 
		[CourseId] INT NOT NULL, 
		[ResponsibilityTypeId] INT NOT NULL, 
		CONSTRAINT [FK_Responsibilities_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]), 
		CONSTRAINT [FK_Responsibilities_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id]), 
		CONSTRAINT [FK_Responsibilities_ResponsibilityType] FOREIGN KEY ([ResponsibilityTypeId]) REFERENCES [ResponsibilityTypes]([Id])
)
