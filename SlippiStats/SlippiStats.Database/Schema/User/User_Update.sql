CREATE PROCEDURE [dbo].[User_Update]
	@id INT,
	@userName VARCHAR(50),
	@password VARCHAR(100),
	@updated DATETIME,
	@deleted DATETIME

AS

UPDATE
	[User]
SET
	UserName = @userName,
	[Password] = @password,
	Updated = @updated,
	Deleted = @deleted
WHERE
	[User].Id = @id