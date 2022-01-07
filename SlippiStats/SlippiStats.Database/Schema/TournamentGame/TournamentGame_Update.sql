
CREATE PROCEDURE [dbo].[TournamentGame_Update]
	@id INT,
	@tournamentId INT,
	@gameId INT,
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME
AS

UPDATE
	TournamentGame
SET
	TournamentId = @tournamentId,
	GameId = @gameId,
	Created = @created,
	Updated = @updated,
	Deleted = @deleted
WHERE
	TournamentGame.Id = @id