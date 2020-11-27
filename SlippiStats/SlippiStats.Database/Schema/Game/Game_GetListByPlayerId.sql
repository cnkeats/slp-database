﻿CREATE PROCEDURE Game_GetListByPlayerId
	@playerId INT

AS

SELECT
	Game.Id,
	Game.Player1,
	Game.Player2,
	Game.Player3,
	Game.Player4,
	Game.Character1,
	Game.Character2,
	Game.Character3,
	Game.Character4,
	Game.Player1Id,
	Game.Player2Id,
	Game.Player3Id,
	Game.Player4Id,
	Game.Winner,
	Game.Stage,
	Game.GameMode,
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
	INNER JOIN Player AS P1 WITH (NOLOCK)
		ON P1.Id = Game.Player1Id
	INNER JOIN Player AS P2 WITH (NOLOCK)
		ON P2.Id = Game.Player2Id
WHERE
	Game.Player1Id = @playerId
	OR Game.Player2Id = @playerId