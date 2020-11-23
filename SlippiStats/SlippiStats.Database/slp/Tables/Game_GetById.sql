CREATE PROCEDURE slp.Game_GetById
	@id INT

AS

SELECT
	Game.Id,
	Game.Player1,
	Game.Player2,
	Game.GameLength,
	Game.Created,
	Game.Updated,
	Game.Deleted	
FROM
	Game WITH (NOLOCK)
WHERE
	Game.Id = @id