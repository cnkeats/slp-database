using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class PlayerProfile
    {
        public Player Player { get; set; }

        public int GamesPlayed { get; set; }

        public int GamesWon { get; set; }

        public int FramesPlayed { get; set; }

        public Character FavoriteCharacter { get; set; }

        public int FavoriteOpponentId { get; set; }

        public Player FavoriteOpponent { get; set; }

        public PlayerProfile()
        {

        }

        private PlayerProfile(IDataReader dataReader)
        {
            GamesPlayed = dataReader.GetValue<int>(nameof(GamesPlayed));
            GamesWon = dataReader.GetValue<int>(nameof(GamesWon));
            FramesPlayed = dataReader.GetValue<int>(nameof(FramesPlayed));
            FavoriteCharacter = (Character)dataReader.GetValue<int>(nameof(FavoriteCharacter));
            FavoriteOpponentId = dataReader.GetValue<int>(nameof(FavoriteOpponent));
        }

        public static PlayerProfile GetByPlayerId(IDbConnection connection, int playerId)
        {
            PlayerProfile playerProfile = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(PlayerProfile)}_{nameof(GetByPlayerId)}",
                new { playerId });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                playerProfile = new PlayerProfile(reader);
            }

            return playerProfile;
        }
    }
}
