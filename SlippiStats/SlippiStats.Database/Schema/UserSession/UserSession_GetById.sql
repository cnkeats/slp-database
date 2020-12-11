CREATE PROCEDURE [dbo].[UserSession_GetById]
	@id UNIQUEIDENTIFIER

AS

SELECT
	UserSession.Id,
	UserSession.Created,
	UserSession.Updated,
	UserSession.Deleted,
	UserSession.Expires,
	UserSession.UserId
FROM
	UserSession WITH (NOLOCK)
WHERE
	UserSession.Id = @id