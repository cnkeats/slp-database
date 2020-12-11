CREATE PROCEDURE [dbo].[User_GetByUserName]
	@userName VARCHAR(50)

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
	[User].UserName = @userName
	AND [User].Deleted IS NULL