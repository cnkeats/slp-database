CREATE PROCEDURE dbo.Player_Insert
	@name VARCHAR(64),
	@connectCode VARCHAR(16),
	@discordCode VARCHAR(16),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

INSERT INTO
	Player
	(
		Name,
		ConnectCode,
		DiscordCode,
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
		@discordCode,
		@created,
		@updated,
		@deleted
	)