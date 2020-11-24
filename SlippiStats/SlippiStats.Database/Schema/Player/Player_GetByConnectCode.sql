﻿CREATE PROCEDURE Player_GetByConnectCode
	@connectCode VARCHAR(16)

AS

SELECT
	Id,
	Name,
	ConnectCode,
	Created,
	Updated,
	Deleted	
FROM
	Player WITH (NOLOCK)
WHERE
	ConnectCode = @connectCode