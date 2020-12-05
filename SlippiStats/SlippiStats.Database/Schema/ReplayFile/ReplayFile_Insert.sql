CREATE PROCEDURE dbo.ReplayFile_Insert
	@gameId INT,
	@uploaderId INT,
	@fileData VARBINARY(MAX),
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

INSERT INTO
	ReplayFile
	(
		GameId,
		UploaderId,
		FileData,
		Created,
		Updated,
		Deleted		
	)
OUTPUT
	inserted.Id
VALUES
	(
		@gameId,
		@uploaderId,
		@fileData,
		@created,
		@updated,
		@deleted
	)