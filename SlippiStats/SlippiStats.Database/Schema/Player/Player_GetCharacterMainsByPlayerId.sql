CREATE PROCEDURE Player_GetCharacterMainsByPlayerId
	@playerId INT

AS

DECLARE @gamesPlayed INT = 
	(
		SELECT
			COUNT(*) AS GamesPlayed
		FROM
			Player WITH (NOLOCK)
			INNER JOIN Game WITH (NOLOCK)
				ON Game.Player1Id = Player.Id
				OR Game.Player2Id = Player.Id
		WHERE
			Player.Id = @playerId
	)

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
HAVING
	COUNT(*) > (@gamesPlayed / 4)
ORDER BY
	Count DESC