using VoiceAcademyWEB.Models;

namespace VoiceAcademyWEB.DTOs
{
    public class LikeslistDTO
    {
        public int IdlikesList { get; set; }

        public int? TotalChapters { get; set; }

        public int? Followers { get; set; }

        public int UserIdUser { get; set; }
        public List<Chapter>? Chapters { get; set; }
    }
}
