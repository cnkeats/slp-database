CREATE TABLE dbo.Tournament (
    Id INT IDENTITY (1, 1) NOT NULL,
    TournamentOrganizerId INT NULL,
    Name VARCHAR(64) NOT NULL,
    StartDate DATE NULL,
    EndDate DATE NULL,
    Created DATETIME NOT NULL,
    Updated DATETIME NULL,
    Deleted DATETIME NULL,
    PRIMARY KEY CLUSTERED (Id ASC)
);