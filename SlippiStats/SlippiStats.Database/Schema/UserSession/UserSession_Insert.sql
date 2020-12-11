CREATE PROCEDURE [dbo].[UserSession_Insert]
	@id UNIQUEIDENTIFIER,
	@created DATETIME,
	@deleted DATETIME = NULL,
	@expires DATETIME,
	@userId INT

AS

INSERT INTO
	UserSession
	(
		Id,
		Created,
		Deleted,
		Expires,
		UserId
	)
VALUES
	(
		@id,
		@created,
		@deleted,
		@expires,
		@userId
	)