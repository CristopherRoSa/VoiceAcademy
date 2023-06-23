using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPI.Business.Interface
{
    public interface IPodcastProvider
    {
        public Task<List<Podcastchannel>> GetPodcasts();
        public Task<Podcastchannel> GetPodcastById(int idPodcast);
        public Task<int> AddPodcast(PodcastDTO newPodcastDTO);
        public Task<int> UpdatePodcast(NewPodcastDTO newPodcastDTO, int idPodcast);
        public Task<int> DeletePodcast(int idPodcast);
        public Task<List<Podcastchannel>> GetPodcastByUserId(int idUser);
        public Task<int> ChangePodcastVisibility(int idPodcast);

    }
}
