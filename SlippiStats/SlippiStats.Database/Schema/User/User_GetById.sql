CREATE PROCEDURE [dbo].[User_GetById]
	@id INT

AS

SELECT
	[User].Id,
	[User].UserName,
	[User].[Password],
	[User].Created,
	[User].Updated,
	[User].Deleted
FROM
	[User] WITH (NOLOCK)
WHERE
	[User].Id = @id