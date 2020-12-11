CREATE PROCEDURE [dbo].[User_Insert]
	@userName VARCHAR(50),
	@password VARCHAR(100),
	@created DATETIME,
	@deleted DATETIME = NULL

AS

INSERT INTO
	[User]
	(
		UserName,
		[Password],
		Created,
		Deleted
	)
OUTPUT
	inserted.Id
VALUES
	(
		@userName,
		@password,
		@created,
		@deleted
	)