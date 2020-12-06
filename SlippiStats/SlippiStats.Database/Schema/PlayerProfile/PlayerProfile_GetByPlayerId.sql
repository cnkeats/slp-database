CREATE PROCEDURE PlayerProfile_GetByPlayerId
	@playerId INT,
	@characterFilter INT,
	@opponentFilter VARCHAR(32),
	@opponentCharacterFilter INT

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
			INNER JOIN Player Opponent WITH (NOLOCK)
				ON (Opponent.Id = Game.Player1Id AND Game.Player1Id <> @playerId)
				OR (Opponent.Id = Game.Player2Id AND Game.Player2Id <> @playerId)
		WHERE
			Player.Id = @playerId
			AND (@opponentFilter IS NULL OR Opponent.Name LIKE @opponentFilter OR Opponent.ConnectCode LIKE @opponentFilter)
			AND (
				@characterFilter IS NULL
				OR (Game.Character1 = @characterFilter AND Game.Player1Id = Player.Id)
				OR (Game.Character2 = @characterFilter AND Game.Player2Id = Player.Id)
			)
			AND (
				@opponentCharacterFilter IS NULL
				OR (Game.Character1 = @opponentCharacterFilter AND Game.Player1Id <> Player.Id)
				OR (Game.Character2 = @opponentCharacterFilter AND Game.Player2Id <> Player.Id)
			)
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
				ON (Opponent.Id = Game.Player1Id AND Game.Player1Id <> @playerId)
				OR (Opponent.Id = Game.Player2Id AND Game.Player2Id <> @playerId)
		WHERE
			Player.Id = @playerId
			AND (@opponentFilter IS NULL OR Opponent.Name LIKE @opponentFilter OR Opponent.ConnectCode LIKE @opponentFilter)
			AND (
				@characterFilter IS NULL
				OR (Game.Character1 = @characterFilter AND Game.Player1Id = Player.Id)
				OR (Game.Character2 = @characterFilter AND Game.Player2Id = Player.Id)
			)
			AND (
				@opponentCharacterFilter IS NULL
				OR (Game.Character1 = @opponentCharacterFilter AND Game.Player1Id <> Player.Id)
				OR (Game.Character2 = @opponentCharacterFilter AND Game.Player2Id <> Player.Id)
			)
		GROUP BY
			Opponent.Id
		ORDER BY
			COUNT(*) DESC
	) AS FavoriteOpponent,
	CAST('3030-06-30' AS DATETIME) AS FirstSpotted,
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
					ON (Opponent.Id = Game.Player1Id AND Game.Player1Id <> @playerId)
					OR (Opponent.Id = Game.Player2Id AND Game.Player2Id <> @playerId)
			WHERE
				Player.Id = @playerId
				AND (@opponentFilter IS NULL OR Opponent.Name LIKE @opponentFilter OR Opponent.ConnectCode LIKE @opponentFilter)
				AND (
					@characterFilter IS NULL
					OR (Game.Character1 = @characterFilter AND Game.Player1Id = Player.Id)
					OR (Game.Character2 = @characterFilter AND Game.Player2Id = Player.Id)
				)
				AND (
					@opponentCharacterFilter IS NULL
					OR (Game.Character1 = @opponentCharacterFilter AND Game.Player1Id <> Player.Id)
					OR (Game.Character2 = @opponentCharacterFilter AND Game.Player2Id <> Player.Id)
				)
			) AS U
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
			INNER JOIN Player Opponent WITH (NOLOCK)
				ON (Opponent.Id = Game.Player1Id AND Game.Player1Id <> @playerId)
				OR (Opponent.Id = Game.Player2Id AND Game.Player2Id <> @playerId)
		WHERE
			Player.Id = @playerId
			AND Game.LRASInitiatorIndex = -1
			AND (@opponentFilter IS NULL OR Opponent.Name LIKE @opponentFilter OR Opponent.ConnectCode LIKE @opponentFilter)
			AND (
				@characterFilter IS NULL
				OR (Game.Character1 = @characterFilter AND Game.Player1Id = Player.Id)
				OR (Game.Character2 = @characterFilter AND Game.Player2Id = Player.Id)
			)
			AND (
				@opponentCharacterFilter IS NULL
				OR (Game.Character1 = @opponentCharacterFilter AND Game.Player1Id <> Player.Id)
				OR (Game.Character2 = @opponentCharacterFilter AND Game.Player2Id <> Player.Id)
			)
	) AS FourStocks,
	-1 AS LRASCount,
	-1 AS OpponentLRASCount,
	@characterFilter AS CharacterFilter,
	@opponentCharacterFilter AS OpponentCharacterFilter,
	@opponentFilter AS OpponentFilter
FROM
	Player WITH (NOLOCK)
	INNER JOIN Game WITH (NOLOCK)
		ON Game.Player1Id = Player.Id
		OR Game.Player2Id = Player.Id
	INNER JOIN Player Opponent WITH (NOLOCK)
		ON (Opponent.Id = Game.Player1Id AND Game.Player1Id <> @playerId)
		OR (Opponent.Id = Game.Player2Id AND Game.Player2Id <> @playerId)
WHERE
	Player.Id = @playerId
	AND (@opponentFilter IS NULL OR Opponent.Name LIKE @opponentFilter OR Opponent.ConnectCode LIKE @opponentFilter)
	AND (
			@characterFilter IS NULL
			OR (Game.Character1 = @characterFilter AND Game.Player1Id = Player.Id)
			OR (Game.Character2 = @characterFilter AND Game.Player2Id = Player.Id)
		)
		AND (
			@opponentCharacterFilter IS NULL
			OR (Game.Character1 = @opponentCharacterFilter AND Game.Player1Id <> Player.Id)
			OR (Game.Character2 = @opponentCharacterFilter AND Game.Player2Id <> Player.Id)
		)
GROUP BY
	Player.Id,
	Player.Name