
CREATE PROCEDURE [dbo].[Tournament_GetById]
	@id INT

AS

SELECT
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
WHERE
	Tournament.Id = @id