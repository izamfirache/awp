CREATE TABLE [dbo].[UserEnrollments]
(
  [Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [UserId] INT NOT NULL, 
    [UserEnrollmentTypeId] INT NOT NULL, 
    [CourseId] INT NOT NULL, 
    CONSTRAINT [FK_UserEnrollments_Users] FOREIGN KEY (UserId) REFERENCES [Users]([Id]),
    CONSTRAINT [FK_UserEnrollments_UserEnrollmentTypes] FOREIGN KEY (UserEnrollmentTypeId) REFERENCES [UserEnrollmentTypes]([Id]),
    CONSTRAINT [FK_UserEnrollments_Courses] FOREIGN KEY (CourseId) REFERENCES [Courses]([Id])
)
