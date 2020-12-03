CREATE PROCEDURE Game_GetDuplicateMatch
	@player1 VARCHAR(64),
	@player2 VARCHAR(64),
	@character1 INT,
	@character2 INT,
	@stage INT,
	@startingSeed BIGINT,
	@gameLength INT

AS

SELECT
	Game.Id,
	Game.Player1Id,
	Game.Player2Id,
	Game.Player3Id,
	Game.Player4Id,
	Game.Character1,
	Game.Character2,
	Game.Character3,
	Game.Character4,
	Game.Player1Victory,
	Game.Player2Victory,
	Game.Player3Victory,
	Game.Player4Victory,
	Game.Stage,
	Game.GameMode,
	Game.GameSettingsId,
	Game.Player1EndingStocks,
	Game.Player2EndingStocks,
	Game.Player3EndingStocks,
	Game.Player4EndingStocks,
	Game.GameEndMethod,
	Game.LRASInitiatorIndex,
	Game.StartAt,
	Game.StartingSeed,
	Game.GameLength,
	Game.FileName,
	Game.FileSource,
	Game.Hash,
	Game.Platform,
	Game.Created,
	Game.Updated,
	Game.Deleted
FROM
	Game WITH (NOLOCK)
	INNER JOIN Player P1 WITH (NOLOCK)
		ON P1.Id = Game.Player1Id
	INNER JOIN Player P2 WITH (NOLOCK)
		ON P2.Id = Game.Player2Id
WHERE
	(@player1 IS NULL OR @player1 = P1.Name)
	AND (@player2 IS NULL OR @player2 = P2.Name)
	AND (@character1 IS NULL OR @character1 = Character1)
	AND (@character2 IS NULL OR @character2 = Character2)
	AND (@stage IS NULL OR @stage = Stage)
	AND (@startingSeed IS NULL OR @startingSeed = StartingSeed)
	AND (@gameLength IS NULL OR @gameLength = GameLength)