CREATE PROCEDURE dbo.ReplayFile_InsertUpdate
	@id INT,
	@gameId INT,
	@uploaderId INT,
	@fileData VARBINARY(MAX),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

UPDATE
	ReplayFile
SET
	GameId = @gameId,
	UploaderId = @uploaderId,
	FileData = @fileData,
	Created = @created,
	Updated = @updated,
	Deleted = @deleted
WHERE
	Id = @id