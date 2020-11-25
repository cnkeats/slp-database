﻿using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace SlippiStats.Models
{
    public class Player
    {
        public int Id { get; private set; }

        public string Name { get; set; }

        public string ConnectCode { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.UtcNow : (DateTime?)null;
        }

        public Player()
        {
            Created = DateTime.Now;
        }

        private Player(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
        }

        public static string GetPlayerName(SlpReplay slpReplay, int playerIndex)
        {
            IDictionary<string, SlpMetadataPlayer> players = slpReplay.MetaData.Players;
            string key = playerIndex.ToString();

            if (players.Keys.Contains(key))
            {
                if (players[key].Names.Netplay != null)
                {
                    return players[key].Names.Netplay;
                }
                else if (players[key].Names.Code != null)
                {
                    return players[key].Names.Code;
                }
                else
                {
                    return slpReplay.Settings.Players[0].Type == PlayerType.HUMAN ? "P" + (playerIndex + 1) : "CPU" + (playerIndex + 1);
                }
            }

            return null;
        }

        public static Player GetByConnectCode(IDbConnection connection, string connectCode)
        {
            Player player = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(GetByConnectCode)}",
                new { connectCode });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                player = new Player(reader);
            }

            return player;
        }

        public void Save(IDbConnection connection)
        {
            if (Id == 0)
            {
                Insert(connection);
            }
            else
            {
                Update(connection);
            }
        }

        private void Insert(IDbConnection connection)
        {
            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(Insert)}",
                new
                {
                    Name,
                    ConnectCode,
                    Created,
                    Updated,
                    Deleted
                });

            Id = (int)command.ExecuteScalar();
        }

        private void Update(IDbConnection connection)
        {
            Updated = DateTime.UtcNow;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Player)}_{nameof(Update)}",
                new
                {
                    Id,
                    Name,
                    ConnectCode,
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}