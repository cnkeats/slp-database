using SlippiStats.GameDataEnums;

namespace SlippiStats.Models
{
    public class SlpSettingsPlayer
    {
        public int PlayerIndex { get; set; }

        public int Port { get; set; }

        public Character CharacterId { get; set; }

        public int CharacterColor { get; set; }

        public int StartStocks { get; set; }

        public PlayerType Type { get; set; }

        public Team TeamId { get; set; }

        public string ControllerFix { get; set; }

        public string Nametag { get; set; }
    }
}
