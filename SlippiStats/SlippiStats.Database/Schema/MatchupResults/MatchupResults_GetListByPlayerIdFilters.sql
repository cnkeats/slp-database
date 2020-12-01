CREATE PROCEDURE MatchupResults_GetListByPlayerIdFilters
	@playerId INT,
	@characterFilter INT,
	@opponentFilter VARCHAR(32),
	@opponentCharacterFilter INT

AS

SET @opponentFilter = '%' + @opponentFilter + '%'

SELECT
	COUNT(*) AS GamesPlayed,
	Character.Name,
	Character.Id AS Character,
	OpponentCharacter.Name AS OpponentCharacterName,
	OpponentCharacter.Id AS OpponentCharacter
FROM
	Player
	INNER JOIN Game
		ON Game.Player1Id = Player.Id
		OR Game.Player2Id = Player.Id
	INNER JOIN Character
		ON (Character.Id = Game.Character1 AND Player.Id = Game.Player1Id)
		OR (Character.Id = Game.Character2 AND Player.Id = Game.Player2Id)
	INNER JOIN Character OpponentCharacter
		ON (OpponentCharacter.Id = Game.Character1 AND Game.Player1Id != @playerId)
		OR (OpponentCharacter.Id = Game.Character2 AND Game.Player2Id != @playerId)
	INNER JOIN Player Opponent WITH (NOLOCK)
		ON (Opponent.Id = Game.Player1Id AND Game.Player1Id <> @playerId)
		OR (Opponent.Id = Game.Player2Id AND Game.Player2Id <> @playerId)
WHERE
	Player.Id = @playerId
	AND (@characterFilter IS NULL OR Character.Id = @characterFilter)
	AND (@opponentFilter IS NULL OR Opponent.Name LIKE @opponentFilter OR Opponent.ConnectCode LIKE @opponentFilter)
	AND (@opponentCharacterFilter IS NULL OR OpponentCharacter.Id = @opponentCharacterFilter)
GROUP BY
	Character.Name,
	Character.Id,
	OpponentCharacter.Name,
	OpponentCharacter.Id
ORDER BY
	GamesPlayed DESC