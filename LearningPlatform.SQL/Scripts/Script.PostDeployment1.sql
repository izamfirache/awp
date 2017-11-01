/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

/*** MERGE-Statement for table [dbo].[Users] ***/

SET IDENTITY_INSERT [dbo].[Users] ON;
GO

MERGE INTO [dbo].[Users] AS Target USING(VALUES 

(1,N'arus',N'123',N'arus@bestemail.eu')
,(2,N'Prometheus',N'123',N'mihai@bestemail.eu')
,(3,N'Zeus',N'123',N'iuliu@bestemail.eu')
) AS Source ([Id],[Username],[Password],[Email])
 ON 
Target.[Id] = Source.[Id] 

 WHEN MATCHED THEN UPDATE SET 
[Username] = Source.[Username] 
,[Password] = Source.[Password] 
,[Email] = Source.[Email] 

 WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id],[Username],[Password],[Email])
VALUES ([Id],[Username],[Password],[Email])
 -- WHEN NOT MATCHED BY SOURCE THEN DELETE  -- uncomment this line to support deletes, too!
;

SET IDENTITY_INSERT [dbo].[Users] OFF;
GO


/*** MERGE-Statement for table [dbo].[Courses] ***/

SET IDENTITY_INSERT [dbo].[Courses] ON;
GO

MERGE INTO [dbo].[Courses] AS Target USING(VALUES 

(1,N'.net Framework. C# fundamentals.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(2,N'Object Oriented Programing. Java fundamentals.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(3,N'Procedural programming.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(4,N'C++ used in the OO way.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(5,N'Python advanced aspects.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(6,N'Javascript fundamentals.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(7,N'Functional Programming aspects. Prolog.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(8,N'Haskell for begginers.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(9,N'Ruby fundamentals.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
,(10,N'HTML and CSS advanced.',N'','20171101 12:57:24.000','20171101 12:57:24.000')
) AS Source ([Id],[Name],[Description],[CreationDate],[UpdateDate])
 ON 
Target.[Id] = Source.[Id] 

 WHEN MATCHED THEN UPDATE SET 
[Name] = Source.[Name] 
,[Description] = Source.[Description] 
,[CreationDate] = Source.[CreationDate] 
,[UpdateDate] = Source.[UpdateDate] 

 WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id],[Name],[Description],[CreationDate],[UpdateDate])
VALUES ([Id],[Name],[Description],[CreationDate],[UpdateDate])
 -- WHEN NOT MATCHED BY SOURCE THEN DELETE  -- uncomment this line to support deletes, too!
;

SET IDENTITY_INSERT [dbo].[Courses] OFF;
GO

SET IDENTITY_INSERT [dbo].[ResponsibilityTypes] ON;
GO

/*** MERGE-Statement for table [dbo].[ResponsibilityTypes] ***/

MERGE INTO [dbo].[ResponsibilityTypes] AS Target USING(VALUES 

(1,N'Owner',N'Creator of the course')
,(2,N'Contributor',N'User that can contribute to the course')
) AS Source ([Id],[Name],[Description])
 ON 
Target.[Id] = Source.[Id] 

 WHEN MATCHED THEN UPDATE SET 
[Name] = Source.[Name] 
,[Description] = Source.[Description] 

 WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id],[Name],[Description])
VALUES ([Id],[Name],[Description])
 -- WHEN NOT MATCHED BY SOURCE THEN DELETE  -- uncomment this line to support deletes, too!
;

SET IDENTITY_INSERT [dbo].[ResponsibilityTypes] OFF;
GO

SET IDENTITY_INSERT [dbo].[Responsibilities] ON;
GO


/*** MERGE-Statement for table [dbo].[Responsibilities] ***/

MERGE INTO [dbo].[Responsibilities] AS Target USING(VALUES 

(1,1,1,1)
,(2,2,2,1)
,(3,3,3,1)
,(4,3,4,1)
,(5,2,5,1)
,(6,1,3,2)
,(7,2,1,2)
,(8,3,2,2)
) AS Source ([Id],[UserId],[CourseId],[ResponsibilityTypeId])
 ON 
Target.[Id] = Source.[Id] 

 WHEN MATCHED THEN UPDATE SET 
[UserId] = Source.[UserId] 
,[CourseId] = Source.[CourseId] 
,[ResponsibilityTypeId] = Source.[ResponsibilityTypeId] 

 WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id],[UserId],[CourseId],[ResponsibilityTypeId])
VALUES ([Id],[UserId],[CourseId],[ResponsibilityTypeId])
 -- WHEN NOT MATCHED BY SOURCE THEN DELETE  -- uncomment this line to support deletes, too!
;

SET IDENTITY_INSERT [dbo].[Responsibilities] OFF;
GO

SET IDENTITY_INSERT [dbo].[Tags] ON;
GO

MERGE INTO [dbo].[Tags] AS Target USING(VALUES 

(1,N'C#')
,(2,N'Javascript')
,(3,N'Java')
,(4,N'Python')
,(5,N'C')
,(6,N'SQL')
,(7,N'HTML')
,(8,N'CSS')
,(9,N'Ruby')
,(10,N'Haskell')
,(11,N'Prolog')
,(12,N'OOP')
,(13,N'C++')
) AS Source ([Id],[TagName])
 ON 
Target.[Id] = Source.[Id] 

 WHEN MATCHED THEN UPDATE SET 
[TagName] = Source.[TagName] 

 WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id],[TagName])
VALUES ([Id],[TagName])
 -- WHEN NOT MATCHED BY SOURCE THEN DELETE  -- uncomment this line to support deletes, too!
;

SET IDENTITY_INSERT [dbo].[Tags] OFF;
GO

/*** MERGE-Statement for table [dbo].[CoursesTags] ***/

SET IDENTITY_INSERT [dbo].[CoursesTags] ON;
GO

MERGE INTO [dbo].[CoursesTags] AS Target USING(VALUES 

(1,1,1)
,(2,1,12)
,(3,2,12)
,(4,4,13)
,(5,4,12)
,(6,5,4)
,(7,6,2)
,(8,7,11)
,(9,8,10)
,(10,9,9)
,(11,10,7)
,(12,10,8)
) AS Source ([Id],[CourseId],[TagId])
 ON 
Target.[Id] = Source.[Id] 

 WHEN MATCHED THEN UPDATE SET 
[CourseId] = Source.[CourseId] 
,[TagId] = Source.[TagId] 

 WHEN NOT MATCHED BY TARGET THEN 
INSERT ([Id],[CourseId],[TagId])
VALUES ([Id],[CourseId],[TagId])
 -- WHEN NOT MATCHED BY SOURCE THEN DELETE  -- uncomment this line to support deletes, too!
;

SET IDENTITY_INSERT [dbo].[CoursesTags] OFF;
GO
