using System;
using System.Collections.Generic;

namespace SlippiStats.Models
{
    public class SlpMetadata
    {
        public DateTime StartAt { get; set; }

        public int LastFrame { get; set; }

        public IDictionary<string, SlpMetadataPlayer> Players { get; set; }

        public string PlayedOn { get; set; }
    }
}
