CREATE PROCEDURE PlayerProfile_GetByPlayerId
	@playerId INT

AS

SELECT
	Player.Id,
	Player.Name,
	COUNT(Game.Id) AS GamesPlayed,
	SUM(CASE WHEN Player1Id = @playerId THEN CAST(Player1Victory AS INT) ELSE CAST(Player2Victory AS INT) END) AS GamesWon,
	SUM(Game.GameLength) AS FramesPlayed,
	SUM(Game.GameLength) / COUNT(Game.Id) / 60 AS AverageGameDuration,
	(
		SELECT TOP 1
			Character.Id
		FROM
			Player WITH (NOLOCK)
			INNER JOIN Game WITH (NOLOCK)
				ON Game.Player1Id = Player.Id
				OR Game.Player2Id = Player.Id
			INNER JOIN Character WITH (NOLOCK)
				ON (Character.Id = Game.Character1 AND Player.Id = Game.Player1Id)
				OR (Character.Id = Game.Character2 AND Player.Id = Game.Player2Id)
		WHERE
			Player.Id = @playerId
		GROUP BY
			Character.Id
		ORDER BY
			COUNT(*) DESC
	) AS FavoriteCharacter,
	(
		SELECT TOP 1
			Opponent.Id
		FROM
			Player WITH (NOLOCK)
			INNER JOIN Game WITH (NOLOCK)
				ON Game.Player1Id = Player.Id
				OR Game.Player2Id = Player.Id
			INNER JOIN Player Opponent WITH (NOLOCK)
				ON Opponent.Id = Game.Player1Id
				OR Opponent.Id = Game.Player2Id
		WHERE
			Player.Id = @playerId
			AND Opponent.Id <> @playerId
		GROUP BY
			Opponent.Id
		ORDER BY
			COUNT(*) DESC
	) AS FavoriteOpponent,
	(
		SELECT TOP 1
			StartAt
		FROM
			Player WITH (NOLOCK)
			INNER JOIN Game WITH (NOLOCK)
				ON Game.Player1Id = Player.Id
				OR Game.Player2Id = Player.Id
		WHERE
			Player.Id = @playerId
		ORDER BY
			Game.StartAt
	) AS FirstSpotted,
	(
		SELECT
			COUNT(U.Id)
		FROM
		(SELECT DISTINCT
			Opponent.Id
		FROM
			Player WITH (NOLOCK)
			INNER JOIN Game WITH (NOLOCK)
				ON Game.Player1Id = Player.Id
				OR Game.Player2Id = Player.Id
			INNER JOIN Player Opponent WITH (NOLOCK)
				ON Opponent.Id = Game.Player1Id
				OR Opponent.Id = Game.Player2Id
		WHERE
			Player.Id = @playerId
			AND Opponent.Id <> @playerId) AS U
	) AS UniqueOpponents,
	(
		SELECT
			COUNT(*)
		FROM
			Player WITH (NOLOCK)
			INNER JOIN Game WITH (NOLOCK)
				ON (Game.Player1Id = @playerId
					AND Game.Player1Victory = 1
						AND Game.Player1EndingStocks = 4
					)
				OR (Game.Player2Id = @playerId
					AND Game.Player2Victory = 1
					AND Game.Player2EndingStocks = 4
				)
		WHERE
			Player.Id = @playerId
			AND Game.LRASInitiatorIndex = -1
	) AS FourStocks,
	(
		SELECT
			COUNT(*)
		FROM
			Player WITH (NOLOCK)
			INNER JOIN Game WITH (NOLOCK)
				ON (Game.Player1Id = @playerId
					AND LRASInitiatorIndex = 0
					)
				OR (Game.Player2Id = @playerId
					AND LRASInitiatorIndex = 1
				)
		WHERE
			Player.Id = @playerId
	) AS QuitOuts,
	(
		SELECT
			COUNT(*)
		FROM
			Player WITH (NOLOCK)
			INNER JOIN Game WITH (NOLOCK)
				ON (Game.Player1Id = @playerId
					AND LRASInitiatorIndex = 1
					)
				OR (Game.Player2Id = @playerId
					AND LRASInitiatorIndex = 0
				)
		WHERE
			Player.Id = @playerId
	) AS QuitOutsAgainst
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