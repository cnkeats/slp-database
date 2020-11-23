CREATE TABLE dbo.Character (
    Id         INT          IDENTITY (1, 1) NOT NULL,
    Name VARCHAR(32),
    PRIMARY KEY CLUSTERED (Id ASC)
);