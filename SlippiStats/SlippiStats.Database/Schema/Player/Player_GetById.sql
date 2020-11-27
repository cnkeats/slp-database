CREATE PROCEDURE Player_GetById
	@playerId INT

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
	Id = @playerId