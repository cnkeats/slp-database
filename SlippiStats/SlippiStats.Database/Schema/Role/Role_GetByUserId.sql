CREATE PROCEDURE [dbo].[Role_GetByUserId]
	@userId INT

AS

SELECT
	[Role].Id,
	[Role].[Name],
	[Role].DisplayOrder
FROM
	[Role] WITH (NOLOCK)
	INNER JOIN UserRole WITH (NOLOCK)
		ON UserRole.RoleId = [Role].Id
	INNER JOIN [User] WITH (NOLOCK)
		ON [User].Id = UserRole.UserId
WHERE
	[User].Id = @userId
ORDER BY
	[Role].DisplayOrder,
	[Role].[Name]