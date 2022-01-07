
CREATE PROCEDURE [dbo].[Tournament_Update]
	@id INT,
	@name VARCHAR(64),
	@tournamentOrganizerId INT,
	@startDate DATE,
	@endDate DATE,
	@created DATETIME,
	@updated DATETIME,
	@deleted DATETIME
AS

UPDATE
	Tournament
SET
	Name = @name,
	TournamentOrganizerId = @tournamentOrganizerId,
	StartDate = @startDate,
	EndDate = @endDate,
	Created = @created,
	Updated = @updated,
	Deleted = @deleted
WHERE
	Tournament.Id = @id