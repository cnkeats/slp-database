CREATE PROCEDURE [dbo].[UserSession_GetLatestByUserId]
	@userId INT

AS

SELECT TOP 1
	UserSession.Id,
	UserSession.Created,
	UserSession.Updated,
	UserSession.Deleted,
	UserSession.Expires,
	UserSession.UserId
FROM
	UserSession WITH (NOLOCK)
WHERE
	UserSession.UserId = @userId
ORDER BY
	UserSession.Created DESC