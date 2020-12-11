CREATE PROCEDURE [dbo].[User_GetListByRole]
	@roleName VARCHAR(50)

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
	INNER JOIN UserRole WITH (NOLOCK)
		ON UserRole.UserId = [User].Id
	INNER JOIN [Role] WITH (NOLOCK)
		ON [Role].Id = UserRole.RoleId
WHERE
	[Role].[Name] = @roleName
	AND [User].Deleted IS NULL