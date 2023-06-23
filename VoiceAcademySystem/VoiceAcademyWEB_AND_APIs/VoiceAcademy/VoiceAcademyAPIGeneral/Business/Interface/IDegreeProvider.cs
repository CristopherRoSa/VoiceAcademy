using VoiceAcademyAPIGeneral.DTOs;

namespace VoiceAcademyAPIGeneral.Business.Interface
{
    public interface IDegreeProvider
    {
        public Task<List<DegreeDTO>> GetDegrees();

        public Task<List<DegreeDTO>> GetDegreesByFacultyId(int FacultyId);
    }
}
