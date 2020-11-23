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
