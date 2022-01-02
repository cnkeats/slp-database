
CREATE PROCEDURE [dbo].[Tournament_GetList]

AS

SELECT TOP 50
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
ORDER BY
	Tournament.StartDate DESC