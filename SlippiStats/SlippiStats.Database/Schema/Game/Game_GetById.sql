CREATE PROCEDURE Game_GetById
	@id INT

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
	Winner,
	Stage,
	GameMode,
	StartAt,
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
	Id = @id