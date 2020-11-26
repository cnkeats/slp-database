﻿CREATE PROCEDURE Game_GetList

AS

SELECT
	Id,
	Player1,
	Player2,
	Player3,
	Player4,
	Character1,
	Character2,
	Character3,
	Character4,
	Player1Id,
	Player2Id,
	Player3Id,
	Player4Id,
	Winner,
	Stage,
	GameMode,
	StartAt,
	StartingSeed,
	GameLength,
	FileName,
	Hash,
	Platform,
	Created,
	Updated,
	Deleted	
FROM
	Game WITH (NOLOCK)