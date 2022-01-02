CREATE TABLE dbo.Tournament (
    Id INT IDENTITY (1, 1) NOT NULL,
    Name VARCHAR(64) NOT NULL,
    TournamentOrganizerId INT NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    Created DATETIME NOT NULL,
    Updated DATETIME NULL,
    Deleted DATETIME NULL,
    PRIMARY KEY CLUSTERED (Id ASC)
);