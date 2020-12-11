CREATE PROCEDURE [dbo].[UserRole_GetListByUserId]
	@userId INT

AS

SELECT
	UserRole.Id,
	UserRole.UserId,
	UserRole.RoleId,
	[Role].[Name] AS RoleName
FROM
	UserRole WITH (NOLOCK)
	JOIN [Role] WITH (NOLOCK) ON
		[Role].Id = UserRole.RoleId
WHERE
	UserRole.UserId = @userId