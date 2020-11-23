CREATE PROCEDURE Game_GetByHash
	@hash VARCHAR(32)

AS

SELECT
	Id,
	Player1,
	Player2,
	Character1,
	Character2,
	StartAt,
	GameLength,
	Hash,
	Platform,
	Created,
	Updated,
	Deleted	
FROM
	Game WITH (NOLOCK)
WHERE
	Hash = @hash