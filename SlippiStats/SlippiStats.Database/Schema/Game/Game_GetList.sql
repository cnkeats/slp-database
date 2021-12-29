﻿
CREATE PROCEDURE [dbo].[Game_GetList]
	@includeAnonymous BIT

AS

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
	INNER JOIN Player P1 WITH (NOLOCK)
		ON P1.Id = Game.Player1Id
	INNER JOIN Player P2 WITH (NOLOCK)
		ON P2.Id = Game.Player2Id
WHERE
	(@includeAnonymous = 1 OR
		(
			P1.Name <> 'P1'
			AND P1.Name <> 'P2'
			AND P1.Name <> 'P3'
			AND P1.Name <> 'P4'
			AND P1.Name <> 'CPU1'
			AND P1.Name <> 'CPU2'
			AND P1.Name <> 'CPU3'
			AND P1.Name <> 'CPU4'
		)
	)
	AND (@includeAnonymous = 1 OR
		(
			P2.Name <> 'P1'
			AND P2.Name <> 'P2'
			AND P2.Name <> 'P3'
			AND P2.Name <> 'P4'
			AND P2.Name <> 'CPU1'
			AND P2.Name <> 'CPU2'
			AND P2.Name <> 'CPU3'
			AND P2.Name <> 'CPU4'
		)
	)
ORDER BY
	Game.StartAt DESC,
	Game.Id