CREATE TABLE [dbo].[CoursesRatings]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [CourseId] INT NULL, 
    [UserId] INT NULL, 
    [Rating] INT NULL, 
    [RatingDate] DATETIME NULL, 
    CONSTRAINT [FK_CoursesRatings_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id]), 
    CONSTRAINT [FK_CoursesRatings_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
)
