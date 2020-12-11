CREATE PROCEDURE [dbo].[Role_GetList]

AS

SELECT
	[Role].Id,
	[Role].[Name],
	[Role].DisplayOrder
FROM
	[Role] WITH (NOLOCK)
ORDER BY
	[Role].DisplayOrder,
	[Role].[Name]