CREATE PROCEDURE Game_Update
	@id INT,
	@player1 VARCHAR(64),
	@player2 VARCHAR(64),
	@character1 VARCHAR(32),
	@character2 VARCHAR(32),
	@startAt DATETIME,
	@gameLength INT,
	@hash VARCHAR(32),
	@platform VARCHAR(32),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

UPDATE
	Game
SET
	Player1 = @player1,
	Player2 = @player2,
	Character1 = @character1,
	Character2 = @character2,
	StartAt = @startAt,
	GameLength = @gameLength,
	Hash = @hash,
	Platform = @platform,
	Created = @created,
	Updated = @updated,
	Deleted = @deleted
WHERE
	Game.Id = @id