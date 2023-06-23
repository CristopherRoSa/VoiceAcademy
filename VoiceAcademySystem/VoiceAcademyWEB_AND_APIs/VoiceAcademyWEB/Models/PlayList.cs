namespace VoiceAcademyWEB.Models
{
    public class PlayList
    {
        public int IdPlayList { get; set; }

        public string Description { get; set; } = null!;

        public DateTime CreationDate { get; set; }

        public string Name { get; set; } = null!;

        public int Followers { get; set; }

        public byte[]? ImagePlayList { get; set; }

        public int? StateList { get; set; }

        public int UvcomunityIdUvcomunity { get; set; }
    }
}
