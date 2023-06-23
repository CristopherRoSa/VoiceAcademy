using VoiceAcademyWEB.Models;

namespace VoiceAcademyWEB.Business.Interface
{
    public interface IChaptersProvider
    {
        public Task<List<Chapter>> GetChaptersByPlaylist(int idPlaylist);
        public Task<List<Chapter>> GetChaptersByPodcast(int idPodcast);
        public Task<bool> UpdateChapter(Chapter chapter);
        public Task<Chapter> GetChapterById(int idChapter);
    }
}
