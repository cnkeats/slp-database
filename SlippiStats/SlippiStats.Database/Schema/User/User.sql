CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] VARCHAR(50) NOT NULL, 
    [Password] VARCHAR(100) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [Updated] DATETIME NULL, 
    [Deleted] DATETIME NULL
)
