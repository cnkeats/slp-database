CREATE PROCEDURE Game_GetListByFilters
	@playerName1 VARCHAR(64),
	@playerName2 VARCHAR(64),
	@characterName1 VARCHAR(32),
	@characterName2 VARCHAR(32),
	@stageName VARCHAR(32)

AS

SET @playerName1 = '%' + @playerName1 + '%'
SET @playerName2 = '%' + @playerName2 + '%'
SET @characterName1 = '%' + @characterName1 + '%'
SET @characterName2 = '%' + @characterName2 + '%'
SET @stageName = '%' + @stageName + '%'

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
	INNER JOIN Character AS C1 WITH (NOLOCK)
		ON C1.Id = Game.Character1
	INNER JOIN Character AS C2 WITH (NOLOCK)
		ON C2.Id = Game.Character2
	INNER JOIN Stage WITH (NOLOCK)
		ON Stage.Id = Game.Stage
WHERE
	(@playerName1 IS NULL OR P1.Name LIKE @playerName1 OR P2.Name LIKE @playerName1 OR P1.ConnectCode LIKE @playerName1 OR P2.ConnectCode LIKE @playerName1)
	AND (@playerName2 IS NULL OR P1.Name LIKE @playerName2 OR P2.Name LIKE @playerName2 OR P1.ConnectCode LIKE @playerName2 OR P2.ConnectCode LIKE @playerName2)
	AND (@characterName1 IS NULL OR C1.Name LIKE @characterName1 OR C2.Name LIKE @characterName1)
	AND (@characterName2 IS NULL OR C1.Name LIKE @characterName2 OR C2.Name LIKE @characterName2)
	AND (@stageName IS NULL OR Stage.Name LIKE @stageName OR Stage.Name LIKE @stageName)