using System.Collections.Generic;

namespace SlippiStats.Models
{
    public class SlpSettings
    {
        public string SlpVersion { get; set; }

        public bool IsTeams { get; set; }

        public bool IsPal { get; set; }

        public int StageId { get; set; }

        public List<SlpSettingsPlayer> Players { get; set; }

        public int Scene { get; set; }

        public int GameMode { get; set; }
    }
}
