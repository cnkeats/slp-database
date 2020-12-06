CREATE PROCEDURE Player_GetGamesWonByPlayerId
	@playerId INT

AS

SELECT
	COUNT(*) AS GamesWon
FROM
	Player WITH (NOLOCK)
	INNER JOIN Game WITH (NOLOCK)
		ON (Game.Player1Id = Player.Id AND Game.Player1Victory = 1)
		OR (Game.Player2Id = Player.Id AND Game.Player2Victory = 1)
WHERE
	Player.Id = @playerId