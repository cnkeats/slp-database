CREATE PROCEDURE [dbo].[User_GetList]

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
	[User].Deleted IS NULL
ORDER BY
	[User].UserName