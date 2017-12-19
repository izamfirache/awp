CREATE TABLE [dbo].[CourseTopicsLinks]
(
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Link] NVARCHAR(MAX) NOT NULL, 
    [CourseTopicId] INT NOT NULL, 
    CONSTRAINT [FK_CourseTopicsLinks_CourseTopics] FOREIGN KEY ([CourseTopicId]) REFERENCES [CourseTopics]([Id])
)
