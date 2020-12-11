CREATE TABLE [dbo].[UserSession]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Created] DATETIME NOT NULL, 
    [Updated] DATETIME NULL, 
    [Deleted] DATETIME NULL, 
    [Expires] DATETIME NOT NULL, 
    [UserId] INT NOT NULL, 
    CONSTRAINT [FK_UserSession_User] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]), 
)