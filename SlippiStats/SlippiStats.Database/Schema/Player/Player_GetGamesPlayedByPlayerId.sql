CREATE PROCEDURE Player_GetGamesPlayedByPlayerId
	@playerId INT

AS

SELECT
	COUNT(*) AS GamesPlayed
FROM
	Player WITH (NOLOCK)
	INNER JOIN Game WITH (NOLOCK)
		ON Game.Player1Id = Player.Id
		OR Game.Player2Id = Player.Id
WHERE
	Player.Id = @playerId