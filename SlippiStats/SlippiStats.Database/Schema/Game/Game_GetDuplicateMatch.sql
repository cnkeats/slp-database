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
	Id,
	Player1,
	Player2,
	Player3,
	Player4,
	Character1,
	Character2,
	Character3,
	Character4,
	Player1Id,
	Player2Id,
	Player3Id,
	Player4Id,
	Winner,
	Stage,
	GameMode,
	StartAt,
	StartingSeed,
	GameLength,
	FileName,
	Hash,
	Platform,
	Created,
	Updated,
	Deleted	
FROM
	Game WITH (NOLOCK)
WHERE
	(@player1 IS NULL OR @player1 = Player1)
	AND (@player2 IS NULL OR @player2 = Player2)
	AND (@character1 IS NULL OR @character1 = Character1)
	AND (@character2 IS NULL OR @character2 = Character2)
	AND (@stage IS NULL OR @stage = Stage)
	AND (@startingSeed IS NULL OR @startingSeed = StartingSeed)
	AND (@gameLength IS NULL OR @gameLength = GameLength)