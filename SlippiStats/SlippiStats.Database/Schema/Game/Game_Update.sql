﻿CREATE PROCEDURE dbo.Game_Update
	@id INT,
	@player1 VARCHAR(64),
	@player2 VARCHAR(64),
	@player3 VARCHAR(64),
	@player4 VARCHAR(64),
	@character1 VARCHAR(32),
	@character2 VARCHAR(32),
	@character3 VARCHAR(32),
	@character4 VARCHAR(32),
	@player1Id INT,
	@player2Id INT,
	@player3Id INT,
	@player4Id INT,
	@winner INT,
	@stage INT,
	@gameMode INT,
	@startAt DATETIME,
	@gameLength INT,
	@fileName VARCHAR(64),
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
	Player3 = @player3,
	Player4 = @player4,
	Character1 = @character1,
	Character2 = @character2,
	Character3 = @character3,
	Character4 = @character4,
	Player1Id = @player1Id,
	Player2Id = @player2Id,
	Player3Id = @player3Id,
	Player4Id = @player4Id,
	Winner = @winner,
	Stage = @stage,
	StartAt = @startAt,
	GameLength = @gameLength,
	FileName = @fileName,
	Hash = @hash,
	Platform = @platform,
	Created = @created,
	Updated = @updated,
	Deleted = @deleted
WHERE
	Game.Id = @id