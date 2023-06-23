using VoiceAcademyWEB.DTOs;

namespace VoiceAcademyWEB.Business.Interface
{
    public interface IDegreeProvider
    {
        public Task<List<DegreeDTO>> GetDegrees();
    }
}
