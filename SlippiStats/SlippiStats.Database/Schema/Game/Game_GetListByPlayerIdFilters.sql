﻿CREATE PROCEDURE [dbo].[Game_GetListByPlayerIdFilters]
	@playerId INT,
	@character INT,
	@opponentFilter VARCHAR(32),
	@opponentCharacter INT,
	@stage INT,
	@opponentPlayerId INT

AS

SET @opponentFilter = '%' + @opponentFilter + '%'

SELECT TOP 50
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
	Game.Deleted,
	P1.Id AS p1Id,
	P1.Name AS p1Name,
	P1.ConnectCode AS p1ConnectCode,
	P1.DiscordCode AS p1DiscordCode,
	P1.Created AS p1Created,
	P1.Updated AS p1Updated,
	P1.Deleted AS p1Deleted,
	P2.Id AS p2Id,
	P2.Name AS p2Name,
	P2.ConnectCode AS p2ConnectCode,
	P2.DiscordCode AS p2DiscordCode,
	P2.Created AS p2Created,
	P2.Updated AS p2Updated,
	P2.Deleted AS p2Deleted
FROM
	Game WITH (NOLOCK)
	INNER JOIN Player Opponent WITH (NOLOCK)
		ON (Opponent.Id = Game.Player1Id AND Game.Player1Id <> @playerId)
		OR (Opponent.Id = Game.Player2Id AND Game.Player2Id <> @playerId)
	INNER JOIN Player AS P1 WITH (NOLOCK)
		ON P1.Id = Game.Player1Id
	INNER JOIN Player AS P2 WITH (NOLOCK)
		ON P2.Id = Game.Player2Id
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