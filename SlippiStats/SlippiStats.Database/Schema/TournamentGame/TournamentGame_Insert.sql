
CREATE PROCEDURE [dbo].[TournamentGame_Insert]
	@tournamentId INT,
	@gameId INT,
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

INSERT INTO
	TournamentGame
	(
		TournamentId,
		GameId,
		Created,
		Updated,
		Deleted
	)
OUTPUT
	inserted.Id
VALUES
	(
		@tournamentId,
		@gameId,
		@created,
		@updated,
		@deleted
	)