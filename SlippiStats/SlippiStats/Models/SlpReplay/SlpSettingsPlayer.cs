using SlippiStats.GameDataEnums;

namespace SlippiStats.Models
{
    public class SlpSettingsPlayer
    {
        public int PlayerIndex { get; set; }

        public int Port { get; set; }

        public Character CharacterId { get; set; }

        public PlayerType Type { get; set; }

        public int StartStocks { get; set; }

        public int CostumeIndex { get; set; }

        public int TeamShade { get; set; }

        public float Handicap { get; set; }

        public Team TeamId { get; set; }

        public PlayerBitfield PlayerBitfield { get; set; }

        public int CPULevel { get; set; }

        public float OffenseRatio { get; set; }

        public float DefenseRatio { get; set; }

        public float ModelScale { get; set; }

        public string ControllerFix { get; set; }

        public string Nametag { get; set; }

        public string DisplayName { get; set; }

        public string ConnectCode { get; set; }
    }
}
