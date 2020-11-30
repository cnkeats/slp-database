﻿using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System;
using System.Data;

namespace SlippiStats.Models
{
    public class PlayerProfile
    {
        public Player Player { get; set; }

        public DateTime FirstSpotted { get; set; }

        public int GamesPlayed { get; set; }

        public int GamesWon { get; set; }

        public int FramesPlayed { get; set; }

        public Character FavoriteCharacter { get; set; }

        public int FavoriteOpponentId { get; set; }

        public Player FavoriteOpponent { get; set; }

        public int FourStocks { get; set; }

        public int UniqueOpponents { get; set; }

        public int QuitOuts { get; set; }

        public int QuitOutsAgainst { get; set; }

        public PlayerProfile()
        {

        }

        private PlayerProfile(IDataReader dataReader)
        {
            FirstSpotted = dataReader.GetValue<DateTime>(nameof(FirstSpotted));
            GamesPlayed = dataReader.GetValue<int>(nameof(GamesPlayed));
            GamesWon = dataReader.GetValue<int>(nameof(GamesWon));
            FramesPlayed = dataReader.GetValue<int>(nameof(FramesPlayed));
            FavoriteCharacter = (Character)dataReader.GetValue<int>(nameof(FavoriteCharacter));
            FavoriteOpponentId = dataReader.GetValue<int>(nameof(FavoriteOpponent));
            UniqueOpponents = dataReader.GetValue<int>(nameof(UniqueOpponents));
            QuitOuts = dataReader.GetValue<int>(nameof(QuitOuts));
            QuitOutsAgainst = dataReader.GetValue<int>(nameof(QuitOutsAgainst));
            FourStocks = dataReader.GetValue<int>(nameof(FourStocks));
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
