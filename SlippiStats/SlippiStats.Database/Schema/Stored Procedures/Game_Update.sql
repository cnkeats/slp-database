CREATE PROCEDURE Game_Update
	@id INT,
	@player1 VARCHAR(64),
	@player2 VARCHAR(64),
	@gameLength INT,
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

UPDATE
	Game
SET
	Player1 = @player1,
	Player1 = @player1,
	GameLength = @gameLength,
	Created = @created,
	Updated = @updated,
	Deleted = @deleted
WHERE
	Game.Id = @id