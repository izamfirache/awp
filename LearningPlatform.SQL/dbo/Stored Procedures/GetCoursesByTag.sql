CREATE PROCEDURE [dbo].[GetCoursesByTag]
	@tagName nvarchar(255)
AS
		select * from dbo.Courses c
  inner join dbo.CoursesTags ct on ct.CourseId = c.Id
  inner join dbo.Tags t on t.Id = ct.TagId
  where t.Name = @tagName
RETURN 0
