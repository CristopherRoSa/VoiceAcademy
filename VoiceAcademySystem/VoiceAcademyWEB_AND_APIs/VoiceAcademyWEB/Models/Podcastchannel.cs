namespace VoiceAcademyWEB.Models
{
    public class Podcastchannel
    {
        public int IdPodcast { get; set; }

        public string Description { get; set; } = null!;

        public int StatePodcast { get; set; }

        public string Topic { get; set; } = null!;

        public string Title { get; set; } = null!;

        public byte[] ImagePodcast { get; set; } = null!;

        public string CreationDate { get; set; }

        public int UvcomunityIdUvcomunity { get; set; }
    }
}
