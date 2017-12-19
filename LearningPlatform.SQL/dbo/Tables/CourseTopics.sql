CREATE TABLE [dbo].[CourseTopics]
(
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [TopicName] NVARCHAR(MAX) NOT NULL, 
    [TopicDescription] NVARCHAR(MAX) NULL, 
    [CourseId] INT NOT NULL, 
    CONSTRAINT [FK_CourseTopics_Courses] FOREIGN KEY ([CourseId]) REFERENCES [Courses]([Id])
)
