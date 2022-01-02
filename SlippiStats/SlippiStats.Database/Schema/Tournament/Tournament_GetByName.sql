
CREATE PROCEDURE [dbo].[Tournament_GetByName]
	@tournamentName VARCHAR(64)

AS

SELECT
	Tournament.Id,
	Tournament.TournamentOrganizerId,
	Tournament.Name,
	Tournament.StartDate,
	Tournament.EndDate,
	Tournament.Created,
	Tournament.Updated,
	Tournament.Deleted
FROM
	Tournament WITH (NOLOCK)
WHERE
	Tournament.Name LIKE '%' + @tournamentName + '%'