using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Models;

namespace VoiceAcademyWEB.Business
{
    public class PodcastChannerlProvider : IPodcastChannelProvider
    {
        HttpClient _cliente;
        TokenDTO _token;
        IMemoryCache _memoryCache;

        public PodcastChannerlProvider(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            var token = memoryCache.Get<TokenDTO>("Token");
            _token = token;
            _memoryCache = memoryCache;
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");

        }
        public Task<bool> AddChapterToPodcast(Chapter chapter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddPodcast(Podcastchannel newPodcast)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteChapterFromPodcast(int idChapter, int idPodcast)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePodcast(int idPodcast)
        {
            throw new NotImplementedException();
        }

        public async Task<Podcastchannel> GetPodcastById(int idPodcast)
        {
            var podcast = new Podcastchannel();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWSPodcast>($"/api/Podcasts/GetPodcastById/{idPodcast}");
                if (response.correct)
                {
                    podcast = response.data;
                }
                else
                {
                    throw new Exception(response.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
            return podcast;
        }

        public async Task<List<Podcastchannel>> GetPodcasts()
        {
            var podcastList = new List<Podcastchannel>();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWSPodcastList>("/api/Podcasts/GetPodcasts");
                if (response.correct)
                {
                    podcastList = response.list;
                }
                else
                {
                    throw new Exception(response.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
            return podcastList;
        }

        public Task<List<Podcastchannel>> GetPodcastsByUser(int idUser)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePodcast(Podcastchannel podcastchannel)
        {
            throw new NotImplementedException();
        }
    }
    class RespuestaWSPodcastList
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
        public List<Podcastchannel> list { get; set; }
    }
    class RespuestaWSPodcast
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
        public Podcastchannel data { get; set; }
    }
}
