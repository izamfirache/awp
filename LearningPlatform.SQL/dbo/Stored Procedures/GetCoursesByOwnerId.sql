CREATE PROCEDURE [dbo].[GetCoursesByOwnerId]
	@ownerId int
AS
	select * from dbo.Courses c
inner join dbo.Responsibilities r on r.CourseId = c.Id
inner join dbo.ResponsibilityTypes rt on rt.Id = r.ResponsibilityTypeId
inner join dbo.Users u on u.Id = r.UserId
where rt.Name = 'Owner' and u.Id = @ownerId
