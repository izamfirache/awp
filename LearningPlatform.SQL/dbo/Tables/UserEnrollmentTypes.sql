CREATE TABLE [dbo].[UserEnrollmentTypes]
(  
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL, 
    CONSTRAINT [PK_UserEnrollmentTypes] PRIMARY KEY ([Id])
)
