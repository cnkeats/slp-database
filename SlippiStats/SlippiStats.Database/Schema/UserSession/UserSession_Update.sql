CREATE PROCEDURE [dbo].[UserSession_Update]
	@id UNIQUEIDENTIFIER,
	@updated DATETIME,
	@deleted DATETIME = NULL,
	@expires DATETIME

AS

UPDATE
	UserSession
SET
	Updated = @updated,
	Deleted = @deleted,
	Expires = @expires
WHERE
	UserSession.Id = @id