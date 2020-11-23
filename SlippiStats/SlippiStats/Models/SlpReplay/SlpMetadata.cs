using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

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
