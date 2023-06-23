using VoiceAcademyWEB.Models;

namespace VoiceAcademyWEB.Business.Interface
{
    public interface IPodcastChannelProvider
    {
        public Task<List<Podcastchannel>> GetPodcasts();
        public Task<Podcastchannel> GetPodcastById(int idPodcast);
        public Task<bool> AddPodcast(Podcastchannel newPodcast);
        public Task<bool> DeletePodcast(int idPodcast);
        public Task<bool> UpdatePodcast(Podcastchannel podcastchannel);
        public Task<List<Podcastchannel>> GetPodcastsByUser(int idUser);
        public Task<bool> AddChapterToPodcast(Chapter chapter);
        public Task<bool> DeleteChapterFromPodcast(int idChapter, int idPodcast);

    }
}
