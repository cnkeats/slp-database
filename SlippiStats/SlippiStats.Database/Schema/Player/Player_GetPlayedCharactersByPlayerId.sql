CREATE PROCEDURE Player_GetPlayedCharactersByPlayerId
	@playerId INT

AS

SELECT
	COUNT(*) AS Count,
	Character.Name,
	Character.Id
FROM
	Player
	INNER JOIN Game
		ON Game.Player1Id = Player.Id
		OR Game.Player2Id = Player.Id
	INNER JOIN Character
		ON (Character.Id = Game.Character1 AND Player.Id = Game.Player1Id)
		OR (Character.Id = Game.Character2 AND Player.Id = Game.Player2Id)
WHERE
	Player.Id = @playerId
GROUP BY
	Character.Name,
	Character.Id
ORDER BY
	Count DESC