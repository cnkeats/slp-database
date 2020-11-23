CREATE TABLE [dbo].[Game] (
    Id         INT          IDENTITY (1, 1) NOT NULL,
    Player1    VARCHAR (64) NULL,
    Player2    VARCHAR (64) NULL,
    Character1 INT NULL,
    Character2 INT NULL,
    Winner INT NULL,
    StartAt DATETIME NOT NULL,
    GameLength INT          NOT NULL,
    Hash VARCHAR(32) NOT NULL,
    Platform VARCHAR(32) NOT NULL,
    Created    DATETIME     NOT NULL,
    Updated    DATETIME     NULL,
    Deleted    DATETIME     NULL,
    PRIMARY KEY CLUSTERED (Id ASC)
);