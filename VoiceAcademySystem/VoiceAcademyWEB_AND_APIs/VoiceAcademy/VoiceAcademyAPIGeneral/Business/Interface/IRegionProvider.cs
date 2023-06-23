using VoiceAcademyAPIGeneral.DTOs;

namespace VoiceAcademyAPIGeneral.Business.Interface
{
    public interface IRegionProvider
    {
        public Task<List<RegionDTO>> GetRegions();

        public Task<List<FacultyDTO>> GetFacultiesByRegionId(int IdRegion);
    }
}
