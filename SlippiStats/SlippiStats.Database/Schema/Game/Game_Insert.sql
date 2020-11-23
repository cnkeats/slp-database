CREATE PROCEDURE [dbo].[Game_Insert]
	@player1 VARCHAR(64),
	@player2 VARCHAR(64),
	@gameLength INT,
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

INSERT INTO
	Game
	(
		Player1,
		Player2,
		GameLength,
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
		@gameLength,
		@created,
		@updated,
		@deleted
	)