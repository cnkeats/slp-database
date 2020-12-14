CREATE PROCEDURE Game_GetListByPlayerIdFilters
	@playerId INT,
	@character INT,
	@opponentFilter VARCHAR(32),
	@opponentCharacter INT,
	@stage INT,
	@opponentPlayerId INT

AS

SET @opponentFilter = '%' + @opponentFilter + '%'

SELECT TOP 100
	Game.Id,
	Game.Player1Id,
	Game.Player2Id,
	Game.Player3Id,
	Game.Player4Id,
	Game.Character1,
	Game.Character2,
	Game.Character3,
	Game.Character4,
	Game.Player1Victory,
	Game.Player2Victory,
	Game.Player3Victory,
	Game.Player4Victory,
	Game.Stage,
	Game.GameMode,
	Game.GameSettingsId,
	Game.Player1EndingStocks,
	Game.Player2EndingStocks,
	Game.Player3EndingStocks,
	Game.Player4EndingStocks,
	Game.GameEndMethod,
	Game.LRASInitiatorIndex,
	Game.StartAt,
	Game.StartingSeed,
	Game.GameLength,
	Game.Version,
	Game.FileName,
	Game.FileSource,
	Game.Hash,
	Game.Platform,
	Game.Created,
	Game.Updated,
	Game.Deleted
FROM
	Game WITH (NOLOCK)
	INNER JOIN Player Opponent WITH (NOLOCK)
		ON (Opponent.Id = Game.Player1Id AND Game.Player1Id <> @playerId)
		OR (Opponent.Id = Game.Player2Id AND Game.Player2Id <> @playerId)
WHERE
	(
		Game.Player1Id = @playerId
		AND (@character IS NULL OR Game.Character1 = @character)
		AND (@opponentFilter IS NULL OR Opponent.Name LIKE @opponentFilter OR Opponent.ConnectCode LIKE @opponentFilter)
		AND (@opponentCharacter IS NULL OR Game.Character2 = @opponentCharacter)
		AND (@stage IS NULL OR Game.Stage = @stage)
		AND (@opponentPlayerId IS NULL OR Game.Player2Id = @opponentPlayerId)
	)
	OR
	(
		Game.Player2Id = @playerId
		AND (@character IS NULL OR Game.Character2 = @character)
		AND (@opponentFilter IS NULL OR Opponent.Name LIKE @opponentFilter OR Opponent.ConnectCode LIKE @opponentFilter)
		AND (@opponentCharacter IS NULL OR Game.Character1 = @opponentCharacter)
		AND (@stage IS NULL OR Game.Stage = @stage)
		AND (@opponentPlayerId IS NULL OR Game.Player1Id = @opponentPlayerId)
	)
ORDER BY
	Game.StartAt DESC,
	Game.Id