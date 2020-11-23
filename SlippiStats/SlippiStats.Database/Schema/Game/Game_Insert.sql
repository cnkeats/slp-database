CREATE PROCEDURE [dbo].[Game_Insert]
	@player1 VARCHAR(64),
	@player2 VARCHAR(64),
	@character1 VARCHAR(32),
	@character2 VARCHAR(32),
	@winner INT,
	@startAt DATETIME,
	@gameLength INT,
	@hash VARCHAR(32),
	@platform VARCHAR(32),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

INSERT INTO
	Game
	(
		Player1,
		Player2,
		Character1,
		Character2,
		Winner,
		StartAt,
		GameLength,
		Hash,
		Platform,
		Created,
		Updated,
		Deleted		
	)
OUTPUT
	inserted.Id
VALUES
	(
		@player1,
		@player2,
		@character1,
		@character2,
		@winner,
		@startAt,
		@gameLength,
		@hash,
		@platform,
		@created,
		@updated,
		@deleted
	)