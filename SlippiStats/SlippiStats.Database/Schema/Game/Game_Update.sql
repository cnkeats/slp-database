﻿CREATE PROCEDURE dbo.Game_Update
	@id INT,
	@player1Id INT,
	@player2Id INT,
	@player3Id INT,
	@player4Id INT,
	@character1 INT,
	@character2 INT,
	@character3 INT,
	@character4 INT,
	@player1Victory BIT,
	@player2Victory BIT,
	@player3Victory BIT,
	@player4Victory BIT,
	@stage INT,
	@gameMode INT,
	@gameEndMethod INT,
	@startAt DATETIME,
	@startingSeed BIGINT,
	@gameLength INT,
	@fileName VARCHAR(32),
	@hash VARCHAR(32),
	@platform VARCHAR(32),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

UPDATE
	Game
SET
	Player1Id = @player1Id,
	Player2Id = @player2Id,
	Player3Id = @player3Id,
	Player4Id = @player4Id,
	Character1 = @character1,
	Character2 = @character2,
	Character3 = @character3,
	Character4 = @character4,
	Player1Victory = @player1Victory,
	Player2Victory = @player2Victory,
	Player3Victory = @player3Victory,
	Player4Victory = @player4Victory,
	Stage = @stage,
	GameMode = @gameMode,
	GameEndMethod = @gameEndMethod,
	StartAt = @startAt,
	StartingSeed = @startingSeed,
	GameLength = @gameLength,
	FileName = @fileName,
	Hash = @hash,
	Platform = @platform,
	Created = @created,
	Updated = @updated,
	Deleted = @deleted
WHERE
	Game.Id = @id