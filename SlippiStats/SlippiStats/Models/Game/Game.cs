using SlippiStats.Util;
using System;
using System.Data;

namespace SlippiStats.Models
{
    public class Game
    {
        public int Id { get; private set; }

        public string FileName { get; set; }

        public string Hash { get; set; }

        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public string Player3 { get; set; }

        public string Player4 { get; set; }

        public Character? Character1 { get; set; }

        public Character? Character2 { get; set; }

        public Character? Character3 { get; set; }

        public Character? Character4 { get; set; }

        public string Winner { get; set; }

        public DateTime StartAt { get; set; }

        public int GameLength { get; set; }

        public string Platform { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.UtcNow : (DateTime?)null;
        }

        public Game()
        {
            Created = DateTime.UtcNow;
        }

        public Game(SlpReplay slpReplay)
        {
            FileName = slpReplay.FileName;
            Hash = slpReplay.Hash;
            StartAt = slpReplay.MetaData.StartAt;
            GameLength = slpReplay.MetaData.LastFrame;
            Platform = slpReplay.MetaData.PlayedOn;
            
            Player1 = slpReplay.MetaData.Players.Keys.Contains("0") ? slpReplay.MetaData.Players["0"]?.Names.Netplay : null;
            Player2 = slpReplay.MetaData.Players.Keys.Contains("1") ? slpReplay.MetaData.Players["1"]?.Names.Netplay : null;
            Player3 = slpReplay.MetaData.Players.Keys.Contains("2") ? slpReplay.MetaData.Players["2"]?.Names.Netplay : null;
            Player4 = slpReplay.MetaData.Players.Keys.Contains("3") ? slpReplay.MetaData.Players["3"]?.Names.Netplay : null;

            if (slpReplay.MetaData.Players.Keys.Contains("0"))
            {
                if (slpReplay.MetaData.Players["0"].Names.Netplay != null)
                {
                    Player1 = slpReplay.MetaData.Players["0"].Names.Netplay;
                }
                else if (slpReplay.MetaData.Players["0"].Names.Code != null)
                {
                    Player1 = slpReplay.MetaData.Players["0"].Names.Code;
                }
                else
                {
                    Player1 = slpReplay.Settings.Players[0].Type == 0 ? "P1" : "CPU1";
                }
            }

            if (slpReplay.MetaData.Players.Keys.Contains("1"))
            {
                if (slpReplay.MetaData.Players["1"].Names.Netplay != null)
                {
                    Player2 = slpReplay.MetaData.Players["1"].Names.Netplay;
                }
                else if (slpReplay.MetaData.Players["1"].Names.Code != null)
                {
                    Player2 = slpReplay.MetaData.Players["1"].Names.Code;
                }
                else
                {
                    Player2 = slpReplay.Settings.Players[1].Type == 0 ? "P2" : "CPU2";
                }
            }

            if (slpReplay.MetaData.Players.Keys.Contains("2"))
            {
                if (slpReplay.MetaData.Players["2"].Names.Netplay != null)
                {
                    Player3 = slpReplay.MetaData.Players["2"].Names.Netplay;
                }
                else if (slpReplay.MetaData.Players["2"].Names.Code != null)
                {
                    Player3 = slpReplay.MetaData.Players["2"].Names.Code;
                }
                else
                {
                    Player3 = slpReplay.Settings.Players[2].Type == 0 ? "P3" : "CPU3";
                }
            }

            if (slpReplay.MetaData.Players.Keys.Contains("3"))
            {
                if (slpReplay.MetaData.Players["3"].Names.Netplay != null)
                {
                    Player4 = slpReplay.MetaData.Players["3"].Names.Netplay;
                }
                else if (slpReplay.MetaData.Players["3"].Names.Code != null)
                {
                    Player4 = slpReplay.MetaData.Players["3"].Names.Code;
                }
                else
                {
                    Player4 = slpReplay.Settings.Players[3].Type == 0 ? "P4" : "CPU4";
                }
            }

            Character1 = (Character?)(slpReplay.Settings.Players.Count > 0 ? slpReplay.Settings.Players[0]?.CharacterId : null);
            Character2 = (Character?)(slpReplay.Settings.Players.Count > 1 ? slpReplay.Settings.Players[1]?.CharacterId : null);
            Character3 = (Character?)(slpReplay.Settings.Players.Count > 2 ? slpReplay.Settings.Players[2]?.CharacterId : null);
            Character4 = (Character?)(slpReplay.Settings.Players.Count > 3 ? slpReplay.Settings.Players[3]?.CharacterId : null);
            
            Winner = SlpReplay.DetermineWinner(slpReplay);

            Created = DateTime.UtcNow;
        }

        private Game(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            FileName = dataReader.GetValue<string>(nameof(FileName));
            Hash = dataReader.GetValue<string>(nameof(Hash));
            /*Player1 = dataReader.GetValue<string>(nameof(Player1));
            Player2 = dataReader.GetValue<string>(nameof(Player2));
            Character1 = (Character)dataReader.GetValue<int>(nameof(Character1));
            Character2 = (Character)dataReader.GetValue<int>(nameof(Character2));
            StartAt = dataReader.GetValue<DateTime>(nameof(StartAt));
            GameLength = dataReader.GetValue<int>(nameof(GameLength));
            Platform = dataReader.GetValue<string>(nameof(Platform));*/
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
        }

        public static Game GetByHash(IDbConnection connection, string hash)
        {
            Game user = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(Game)}_{nameof(GetByHash)}",
                new { hash });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                user = new Game(reader);
            }

            return user;
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
                $"{nameof(Game)}_{nameof(Insert)}",
                new
                {
                    Player1,
                    Player2,
                    Player3,
                    Player4,
                    Character1,
                    Character2,
                    Character3,
                    Character4,
                    Winner,
                    StartAt,
                    GameLength,
                    FileName,
                    Hash,
                    Platform,
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
                $"{nameof(Game)}_{nameof(Update)}",
                new
                {
                    Id,
                    Player1,
                    Player2,
                    Player3,
                    Player4,
                    Character1,
                    Character2,
                    Character3,
                    Character4,
                    Winner,
                    StartAt,
                    GameLength,
                    FileName,
                    Hash,
                    Platform,
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}
