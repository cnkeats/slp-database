CREATE PROCEDURE dbo.Player_Insert
	@name VARCHAR(64),
	@connectCode VARCHAR(16),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

INSERT INTO
	Player
	(
		Name,
		ConnectCode,
		Created,
		Updated,
		Deleted		
	)
OUTPUT
	inserted.Id
VALUES
	(
		@name,
		@connectCode,
		@created,
		@updated,
		@deleted
	)