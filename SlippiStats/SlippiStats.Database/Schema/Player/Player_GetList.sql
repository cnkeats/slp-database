CREATE PROCEDURE Player_GetList
	@includeAnonymous BIT

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
	(@includeAnonymous = 1 OR
		(
			Name <> 'P1'
			AND Name <> 'P2'
			AND Name <> 'P3'
			AND Name <> 'P4'
			AND Name <> 'CPU1'
			AND Name <> 'CPU2'
			AND Name <> 'CPU3'
			AND Name <> 'CPU4'
		)
	)