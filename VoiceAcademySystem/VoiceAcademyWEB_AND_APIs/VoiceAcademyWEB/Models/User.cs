namespace VoiceAcademyWEB.Models
{
    public class User
    {
        public int IdUser { get; set; }

        public string Email { get; set; } = null!;

        public int Age { get; set; }

        public string Name { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int StatusUser { get; set; }

        public byte[]? ImageUser { get; set; }

        public string Rol { get; set; } = null!;
    }
}
