﻿CREATE PROCEDURE dbo.Game_Insert
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
	@gameSettingsId INT,
	@player1EndingStocks INT,
	@player2EndingStocks INT,
	@player3EndingStocks INT,
	@player4EndingStocks INT,
	@gameEndMethod INT,
	@LRASInitiatorIndex INT,
	@startAt DATETIME,
	@startingSeed BIGINT,
	@gameLength INT,
	@version VARCHAR(16),
	@fileName VARCHAR(32),
	@fileSource VARCHAR(32),
	@hash VARCHAR(32),
	@platform VARCHAR(32),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

INSERT INTO
	Game
	(
		Player1Id,
		Player2Id,
		Player3Id,
		Player4Id,
		Character1,
		Character2,
		Character3,
		Character4,
		Player1Victory,
		Player2Victory,
		Player3Victory,
		Player4Victory,
		Stage,
		GameMode,
		GameSettingsId,
		Player1EndingStocks,
		Player2EndingStocks,
		Player3EndingStocks,
		Player4EndingStocks,
		GameEndMethod,
		LRASInitiatorIndex,
		StartAt,
		StartingSeed,
		GameLength,
		Version,
		FileName,
		FileSource,
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
		@player1Id,
		@player2Id,
		@player3Id,
		@player4Id,
		@character1,
		@character2,
		@character3,
		@character4,
		@player1Victory,
		@player2Victory,
		@player3Victory,
		@player4Victory,
		@stage,
		@gameMode,
		@gameSettingsId,
		@player1EndingStocks,
		@player2EndingStocks,
		@player3EndingStocks,
		@player4EndingStocks,
		@gameEndMethod,
		@LRASInitiatorIndex,
		@startAt,
		@startingSeed,
		@gameLength,
		@version,
		@fileName,
		@fileSource,
		@hash,
		@platform,
		@created,
		@updated,
		@deleted
	)