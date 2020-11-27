CREATE PROCEDURE Game_GetList
	@includeAnonymous BIT

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
	(@includeAnonymous = 1 OR
		(
			Player1 <> 'P1'
			AND Player1 <> 'P2'
			AND Player1 <> 'P3'
			AND Player1 <> 'P4'
			AND Player1 <> 'CPU1'
			AND Player1 <> 'CPU2'
			AND Player1 <> 'CPU3'
			AND Player1 <> 'CPU4'
		)
	)
	AND (@includeAnonymous = 1 OR
		(
			Player2 <> 'P1'
			AND Player2 <> 'P2'
			AND Player2 <> 'P3'
			AND Player2 <> 'P4'
			AND Player2 <> 'CPU1'
			AND Player2 <> 'CPU2'
			AND Player2 <> 'CPU3'
			AND Player2 <> 'CPU4'
		)
	)