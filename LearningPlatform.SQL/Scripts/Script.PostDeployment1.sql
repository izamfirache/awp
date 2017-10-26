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


SET IDENTITY_INSERT [dbo].[Courses] ON;
GO

/*** MERGE-Statement for table [dbo].[Courses] ***/

MERGE INTO [dbo].[Courses] AS Target USING(VALUES 

(1,N'Humanist Civilization: Synthesis and Development',N'','20171025 13:55:36.000','20171025 13:55:36.000')
,(2,N'Radical American Symbols Since 1881',N'','20171025 13:55:36.000','20171025 13:55:36.000')
,(3,N'Foundations Of World War II: The Jenkins Theory At Work',N'','20171025 13:55:36.000','20171025 13:55:36.000')
,(4,N'Radical Evolution In Recent Times',N'','20171025 13:55:36.000','20171025 13:55:36.000')
,(5,N'Contemporary Japanese Images & Traditions',N'','20171025 13:55:36.000','20171025 13:55:36.000')
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
