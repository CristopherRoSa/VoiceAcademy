namespace VoiceAcademyAPIGeneral.DTOs
{
    public class ChapterDTO
    {
        public int IdChapter { get; set; }

        public string Description { get; set; } = null!;

        public int StateChapter { get; set; }

        public DateTime? PublicationDate { get; set; }

        public string Title { get; set; } = null!;

        public string Topic { get; set; } = null!;

        public byte[]? ImageChapter { get; set; }

        public string? LinkChapter { get; set; }

        public int PodcastIdPodcast { get; set; }

    }
}
