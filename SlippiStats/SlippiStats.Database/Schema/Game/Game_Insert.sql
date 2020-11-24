CREATE PROCEDURE [dbo].[Game_Insert]
	@player1 VARCHAR(64),
	@player2 VARCHAR(64),
	@player3 VARCHAR(64),
	@player4 VARCHAR(64),
	@character1 VARCHAR(32),
	@character2 VARCHAR(32),
	@character3 VARCHAR(32),
	@character4 VARCHAR(32),
	@winner VARCHAR(4),
	@stage INT,
	@gameMode INT,
	@startAt DATETIME,
	@gameLength INT,
	@fileName VARCHAR(32),
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
		Player3,
		Player4,
		Character1,
		Character2,
		Character3,
		Character4,
		Winner,
		Stage,
		GameMode,
		StartAt,
		GameLength,
		FileName,
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
		@player3,
		@player4,
		@character1,
		@character2,
		@character3,
		@character4,
		@winner,
		@stage,
		@gameMode,
		@startAt,
		@gameLength,
		@fileName,
		@hash,
		@platform,
		@created,
		@updated,
		@deleted
	)