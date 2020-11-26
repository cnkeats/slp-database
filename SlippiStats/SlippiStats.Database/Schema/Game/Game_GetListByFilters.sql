CREATE PROCEDURE Game_GetListByFilters
	@playerName1 VARCHAR(64),
	@playerName2 VARCHAR(64),
	@character1 INT,
	@character2 INT,
	@stage INT

AS

SET @playerName1 = '%' + @playerName1 + '%'
SET @playerName2 = '%' + @playerName2 + '%'

SELECT
	Game.Id,
	Game.Player1,
	Game.Player2,
	Game.Player3,
	Game.Player4,
	Game.Character1,
	Game.Character2,
	Game.Character3,
	Game.Character4,
	Game.Player1Id,
	Game.Player2Id,
	Game.Player3Id,
	Game.Player4Id,
	Game.Winner,
	Game.Stage,
	Game.GameMode,
	Game.StartAt,
	Game.StartingSeed,
	Game.GameLength,
	Game.FileName,
	Game.Hash,
	Game.Platform,
	Game.Created,
	Game.Updated,
	Game.Deleted
FROM
	Game WITH (NOLOCK)
	INNER JOIN Player AS P1 WITH (NOLOCK)
		ON P1.Id = Game.Player1Id
	INNER JOIN Player AS P2 WITH (NOLOCK)
		ON P2.Id = Game.Player2Id
WHERE
	(@playerName1 IS NULL OR P1.Name LIKE @playerName1 OR P2.Name LIKE @playerName1 OR P1.ConnectCode LIKE @playerName1 OR P2.ConnectCode LIKE @playerName1)
	AND (@playerName2 IS NULL OR P1.Name LIKE @playerName2 OR P2.Name LIKE @playerName2 OR P1.ConnectCode LIKE @playerName2 OR P2.ConnectCode LIKE @playerName2)
	AND (@character1 IS NULL OR Game.Character1 = @character1 OR Game.Character2 = @character1)
	AND (@character2 IS NULL OR Game.Character1 = @character2 OR Game.Character2 = @character2)
	AND (@stage IS NULL OR Game.Stage = @stage)