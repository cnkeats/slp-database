CREATE PROCEDURE [dbo].[User_IsUserNameInUse]
	@userName VARCHAR(50),
	@userId INT = NULL

AS

DECLARE @isEmailInUse BIT = 0

SELECT
	@isEmailInUse = 1
FROM
	[User] WITH (NOLOCK)
WHERE
	[User].UserName = @userName AND
	(@userId IS NULL OR [User].Id != @userId) AND
	[User].Deleted IS NULL

SELECT @isEmailInUse AS IsEmailInUse