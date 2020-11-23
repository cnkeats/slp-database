using SlippiStats.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SlippiStats.Models
{
    public class SlpReplay
    {
        public int Id { get; private set; }

        public string Hash { get; set; }

        public SlpSettings Settings { get; set; }

        public SlpMetadata MetaData { get; set; }

    }
}
