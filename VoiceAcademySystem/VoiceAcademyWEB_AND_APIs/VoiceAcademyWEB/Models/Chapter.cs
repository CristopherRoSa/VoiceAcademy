namespace VoiceAcademyWEB.Models
{
    public class Chapter
    {
        public int IdChapter { get; set; }

        public string Description { get; set; } = null!;

        public int StateChapter { get; set; }

        public string? PublicationDate { get; set; }

        public string Title { get; set; } = null!;
        public string LinkChapter { get; set; } = null!;

        public string Topic { get; set; } = null!;

        public byte[] ImageChapter { get; set; } = null!;

        public int PodcastIdPodcast { get; set; }
    }
}
