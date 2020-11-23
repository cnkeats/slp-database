CREATE PROCEDURE Game_GetById
	@id INT

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
	Created,
	Updated,
	Deleted	
FROM
	Game WITH (NOLOCK)
WHERE
	Id = @id