CREATE PROCEDURE dbo.ReplayFile_GetByGameId
	@gameId INT

AS

SELECT TOP 1
	Id,
	GameId,
	UploaderId,
	FileData,
	Created,
	Updated,
	Deleted
FROM
	ReplayFile
WHERE
	GameId = @gameId