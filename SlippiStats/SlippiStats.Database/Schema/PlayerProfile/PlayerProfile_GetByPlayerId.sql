CREATE PROCEDURE PlayerProfile_GetByPlayerId
	@playerId INT

AS

SELECT
	Player.Id,
	Player.Name,
	COUNT(*) AS GamesPlayed,
	SUM(CASE WHEN Player1Id = @playerId THEN CAST(Player1Victory AS INT) ELSE CAST(Player2Victory AS INT) END) AS GamesWon,
	SUM(Game.GameLength) AS FramesPlayed
FROM
	Player WITH (NOLOCK)
	INNER JOIN Game WITH (NOLOCK)
		ON Game.Player1Id = Player.Id
		OR Game.Player2Id = Player.Id
WHERE
	Player.Id = @playerId
GROUP BY
	Player.Id,
	Player.Name