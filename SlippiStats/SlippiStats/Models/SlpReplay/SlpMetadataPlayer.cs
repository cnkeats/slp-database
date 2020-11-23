using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SlippiStats.Models
{
    public class SlpMetadataPlayer
    {
        public SlpMetadataNames Names { get; set; }

        public IDictionary<string, int> Characters { get; set; }
    }
}
