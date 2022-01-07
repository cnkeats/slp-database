CREATE TABLE dbo.TournamentGame (
    Id INT IDENTITY (1, 1) NOT NULL,
    TournamentId INT NOT NULL,
        FOREIGN KEY (TournamentId) REFERENCES Tournament(Id),
    GameId INT NOT NULL,
        FOREIGN KEY (GameId) REFERENCES Game(Id),
    Created DATETIME NOT NULL,
    Updated DATETIME NULL,
    Deleted DATETIME NULL,
    PRIMARY KEY CLUSTERED (Id ASC)
);