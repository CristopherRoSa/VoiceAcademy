namespace VoiceAcademyWEB.Models
{
    public class Uvcomunity
    {
        public int IdUvcomunity { get; set; }

        public string InstitutionalEmail { get; set; } = null!;

        public string StudentNumber { get; set; } = null!;

        public int UserIdUser { get; set; }

        public int DegreeIdDegree { get; set; }
    }
}
