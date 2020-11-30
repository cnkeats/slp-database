CREATE PROCEDURE Game_GetListByPlayerIdFilters
	@playerId INT,
	@character INT,
	@opponentCharacter INT,
	@stage INT,
	@opponentPlayerId INT

AS

SELECT
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
	Game.GameEndMethod,
	Game.StartAt,
	Game.StartingSeed,
	Game.GameLength,
	Game.FileName,
	Game.Hash,
	Game.Platform,
	Game.Created,
	Game.Updated,
	Game.Deleted
FROM
	Game WITH (NOLOCK)
WHERE
	(
		Game.Player1Id = @playerId
		AND (@character IS NULL OR Game.Character1 = @character)
		AND (@opponentCharacter IS NULL OR Game.Character2 = @opponentCharacter)
		AND (@stage IS NULL OR Game.Stage = @stage)
		AND (@opponentPlayerId IS NULL OR Game.Player2Id = @opponentPlayerId)
	)
	OR
	(
		Game.Player2Id = @playerId
		AND (@character IS NULL OR Game.Character2 = @character)
		AND (@opponentCharacter IS NULL OR Game.Character1 = @opponentCharacter)
		AND (@stage IS NULL OR Game.Stage = @stage)
		AND (@opponentPlayerId IS NULL OR Game.Player1Id = @opponentPlayerId)
	)
ORDER BY
	Game.StartAt DESC,
	Game.Id