CREATE PROCEDURE Game_GetById
	@id INT

AS

SELECT
	Id,
	Player1,
	Player2,
	Character1,
	Character2,
	Winner,
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
	Id = @id