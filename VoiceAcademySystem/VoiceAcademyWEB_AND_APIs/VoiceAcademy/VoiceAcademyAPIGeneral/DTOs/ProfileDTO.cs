using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPIGeneral.DTOs
{
    public class ProfileDTO
    {
        public int IdUser { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public byte[]? ImagenUser { get; set; }
        public string Email { get; set; }
        public List<Playlist>? playLists { get; set; }
        public Likeslist? likesList { get; set; }
        public List<Podcastchannel>? podcastchannels { get; set; }
        public Uvcomunity? uvcomunity { get; set; }
    }
}
