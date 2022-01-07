CREATE TABLE dbo.Game (
    Id INT IDENTITY (1, 1) NOT NULL,
    Player1Id INT NULL,
    Player2Id INT NULL,
    Player3Id INT NULL,
    Player4Id INT NULL,
    Character1 INT NULL,
    Character2 INT NULL,
    Character3 INT NULL,
    Character4 INT NULL,
    Player1Victory BIT NULL,
    Player2Victory BIT NULL,
    Player3Victory BIT NULL,
    Player4Victory BIT NULL,
    Stage INT NOT NULL,
    GameMode INT NULL,
    GameSettingsId INT NULL,
    Player1EndingStocks INT NULL,
    Player2EndingStocks INT NULL,
    Player3EndingStocks INT NULL,
    Player4EndingStocks INT NULL,
    GameEndMethod INT NULL,
    LRASInitiatorIndex INT NULL,
    StartAt DATETIME NOT NULL,
    StartingSeed BIGINT NULL,
    GameLength INT NOT NULL,
    Version VARCHAR(16) NULL,
    FileName VARCHAR(32) NULL,
    FileSource VARCHAR(32) NOT NULL,
    Hash VARCHAR(32) NULL,
    Platform VARCHAR(32) NOT NULL,
    Created DATETIME NOT NULL,
    Updated DATETIME NULL,
    Deleted DATETIME NULL,
    -- Don't allow games that appear to have players playing themselves, as this shouldn't be possible
    -- Allow them if both players are NULL
    CONSTRAINT NoDuplicatePlayers CHECK (
        (Player1Id <> Player2Id OR Player1Id IS NULL)
        AND (Player1Id <> Player3Id OR Player1Id IS NULL)
        AND Player1Id <> Player4Id
        AND Player2Id <> Player3Id
        AND Player2Id <> Player4Id
        AND Player3Id <> Player4Id
    ),
    PRIMARY KEY CLUSTERED (Id ASC)
);