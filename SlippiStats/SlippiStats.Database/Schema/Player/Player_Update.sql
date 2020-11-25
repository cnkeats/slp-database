﻿CREATE PROCEDURE dbo.Player_Update
	@id INT,
	@name VARCHAR(64),
	@connectCode VARCHAR(16),
	@discordCode VARCHAR(16),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

UPDATE
	Player
SET
	Name = @name,
	ConnectCode = @connectCode,
	DiscordCode = @discordCode,
	Created = @created,
	Updated = @updated,
	Deleted = @deleted
WHERE
	Id = @id