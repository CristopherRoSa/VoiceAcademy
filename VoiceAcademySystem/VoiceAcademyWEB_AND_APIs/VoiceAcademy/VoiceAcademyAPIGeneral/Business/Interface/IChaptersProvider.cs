using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPI.Business.Interface
{
    public interface IChaptersProvider
    {
        public Task<int> AddChapter(ChapterDTO newChapter);

        public Task<bool> DeleteChapter(int idChapter);

        public Task<Chapter> GetChapter(int idChapter);

        public Task<List<Chapter>> GetChapters();

        public Task<List<Chapter>> GetChaptersByPodcast(int idPodcast);

        public Task<bool> UpdateChapter(NewChapterDTO newChapter);
        public Task<List<Chapter>> GetChaptersByPlaylist(int idPlaylist);
        public Task<Chapter> GetChapterById(int idChapter);
        public Task<List<Chapter>> GetLikedChaptersByPodcastId(int idChapter, int idUser);
        public Task<List<Chapter>> GetLikedChaptersByUserId(int idUser);
    }
}
