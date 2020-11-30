CREATE PROCEDURE Player_GetListByFilters
	@playerFilter VARCHAR(32)

AS

SET @playerFilter = '%' + @playerFilter + '%'

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
	@playerFilter IS NULL
	OR (Name LIKE @playerFilter)
	OR (ConnectCode LIKE @playerFilter)
	OR (DiscordCode LIKE @playerFilter)