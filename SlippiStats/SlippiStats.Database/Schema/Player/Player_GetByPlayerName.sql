CREATE PROCEDURE Player_GetByPlayerName
	@playerName VARCHAR(16)

AS

SELECT
	Id,
	Name,
	ConnectCode,
	DiscordCode,
	Created,
	Updated,
	Deleted	
FROM
	Player WITH (NOLOCK)
WHERE
	Name = @playerName