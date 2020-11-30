CREATE PROCEDURE Player_GetList
	@includeAnonymous BIT

AS

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
	(@includeAnonymous = 1 OR
		(
			Name <> 'P1'
			AND Name <> 'P2'
			AND Name <> 'P3'
			AND Name <> 'P4'
			AND Name <> 'CPU1'
			AND Name <> 'CPU2'
			AND Name <> 'CPU3'
			AND Name <> 'CPU4'
		)
	)
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