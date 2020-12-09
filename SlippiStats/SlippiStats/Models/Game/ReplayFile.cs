using Microsoft.AspNetCore.Http;
using SlippiStats.GameDataEnums;
using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SlippiStats.Models
{
    public class ReplayFile
    {
        public int Id { get; private set; }

        public int GameId { get; set; }

        public int UploaderId { get; set; }

        public byte[] FileData { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Deleted { get; set; }

        public bool IsDeleted
        {
            get => Deleted != null;
            set => Deleted = value ? DateTime.Now : (DateTime?)null;
        }

        public ReplayFile(IFormFile formFile, int gameId, int uploaderId = 0)
        {
            try
            {
                if (formFile.FileName != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        formFile.OpenReadStream().CopyTo(ms);
                        FileData = ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }

            GameId = gameId;
            UploaderId = uploaderId;

            Created = DateTime.Now;
        }

        private ReplayFile(IDataReader dataReader)
        {
            Id = dataReader.GetValue<int>(nameof(Id));
            GameId = dataReader.GetValue<int>(nameof(GameId));
            UploaderId = dataReader.GetValue<int>(nameof(UploaderId));
            FileData = dataReader.GetValue<byte[]>(nameof(FileData));
            Created = dataReader.GetValue<DateTime>(nameof(Created));
            Updated = dataReader.GetValue<DateTime?>(nameof(Updated));
            Deleted = dataReader.GetValue<DateTime?>(nameof(Deleted));
        }

        public static ReplayFile GetByGameId(IDbConnection connection, int gameId)
        {
            ReplayFile replayFile = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(ReplayFile)}_{nameof(GetByGameId)}",
                new { gameId });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                replayFile = new ReplayFile(reader);
            }

            return replayFile;
        }

        public static ReplayFile GetByGameIdUploaderId(IDbConnection connection, int gameId, int uploaderId)
        {
            ReplayFile replayFile = null;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(ReplayFile)}_{nameof(GetByGameIdUploaderId)}",
                new { gameId, uploaderId });

            using IDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                replayFile = new ReplayFile(reader);
            }

            return replayFile;
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
                $"{nameof(ReplayFile)}_{nameof(Insert)}",
                new
                {
                    GameId,
                    UploaderId,
                    FileData,
                    Created,
                    Updated,
                    Deleted
                });

            Id = (int)command.ExecuteScalar();
        }

        private void Update(IDbConnection connection)
        {
            Updated = DateTime.Now;

            using IDbCommand command = connection.CreateStoredProcedure(
                $"{nameof(ReplayFile)}_{nameof(Update)}",
                new
                {
                    Id,
                    GameId,
                    UploaderId,
                    FileData,
                    Created,
                    Updated,
                    Deleted
                });

            command.ExecuteNonQuery();
        }
    }
}