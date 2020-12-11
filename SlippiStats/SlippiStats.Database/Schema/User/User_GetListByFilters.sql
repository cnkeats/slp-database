CREATE PROCEDURE [dbo].[User_GetListByFilters]
	@userName VARCHAR(50)

AS

SET @userName = '%' + @userName + '%'

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
	[User].Deleted IS NULL AND 
	(@userName IS NULL OR [User].UserName LIKE @userName)
ORDER BY
	[User].UserName