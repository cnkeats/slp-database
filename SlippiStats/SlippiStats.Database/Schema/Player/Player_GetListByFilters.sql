CREATE PROCEDURE Player_GetListByFilters
	@playerFilter VARCHAR(32)

AS

SET @playerFilter = '%' + @playerFilter + '%'

SELECT
	Player.Id,
	Player.Name,
	Player.ConnectCode,
	Player.DiscordCode,
	Player.Created,
	Player.Updated,
	Player.Deleted,
	COUNT(*) AS GamesPlayed
FROM
	Player WITH (NOLOCK)
	INNER JOIN Game WITH (NOLOCK)
		ON Game.Player1Id = Player.Id
		OR Game.Player2Id = Player.Id
WHERE
	@playerFilter IS NULL
	OR (Name LIKE @playerFilter)
	OR (ConnectCode LIKE @playerFilter)
	OR (DiscordCode LIKE @playerFilter)
GROUP BY
	Player.Id,
	Player.Name,
	Player.ConnectCode,
	Player.DiscordCode,
	Player.Created,
	Player.Updated,
	Player.Deleted
ORDER BY
	GamesPlayed DESC