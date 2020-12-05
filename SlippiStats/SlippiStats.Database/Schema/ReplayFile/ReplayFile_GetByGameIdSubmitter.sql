CREATE PROCEDURE dbo.ReplayFile_GetByGameIdUploaderId
	@gameId INT,
	@uploaderId INT

AS

SELECT
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
	AND UploaderId = @uploaderId