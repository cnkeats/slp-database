using System.Collections.Generic;

namespace SlippiStats.Models
{
    public class SlpMetadataPlayer
    {
        public SlpMetadataNames Names { get; set; }

        public IDictionary<string, int> Characters { get; set; }
    }
}
