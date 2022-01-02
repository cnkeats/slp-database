
CREATE PROCEDURE [dbo].[Tournament_Update]

AS

SELECT TOP 50
	Tournament.Id,
	Tournament.Name,
	Tournament.TournamentOrganizerId,
	Tournament.StartDate,
	Tournament.EndDate,
	Tournament.Created,
	Tournament.Updated,
	Tournament.Deleted
FROM
	Tournament WITH (NOLOCK)
ORDER BY
	Tournament.StartDate DESC