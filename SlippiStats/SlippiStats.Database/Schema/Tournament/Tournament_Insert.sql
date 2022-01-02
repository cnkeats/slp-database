
CREATE PROCEDURE [dbo].[Tournament_Insert]
	@name VARCHAR(64),
	@tournamentOrganizerId INT,
	@startDate DATE,
	@endDate DATE,
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME

AS

INSERT INTO
	Tournament
	(
		Name,
		TournamentOrganizerId,
		StartDate,
		EndDate,
		Created,
		Updated,
		Deleted
	)
OUTPUT
	inserted.Id
VALUES
	(
		@name,
		@tournamentOrganizerId,
		@startDate,
		@endDate,
		@created,
		@updated,
		@deleted
	)