using Microsoft.Extensions.Caching.Memory;
using System.Text.Json.Serialization;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Models;

namespace VoiceAcademyWEB.Business
{
    public class ChaptersProvider : IChaptersProvider
    {
        HttpClient _cliente;
        TokenDTO _token;

        public ChaptersProvider(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            var token = memoryCache.Get<TokenDTO>("Token");
            _token = token;
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");

        }

        public async Task<Chapter> GetChapterById(int idChapter)
        {
            var chapter = new Chapter();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWSobj>($"/api/Chapters/GetChapterById/{idChapter}");
                if (response.correct)
                {
                    chapter = response.data;
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
            return chapter;
        }

        public async Task<List<Chapter>> GetChaptersByPlaylist(int idPlaylist)
        {
            var listchapters = new List<Chapter>();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWSList>($"/api/Chapters/GetChaptersByPlaylist/{idPlaylist}");
                if (response.correct)
                {
                    listchapters = response.list;
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
            return listchapters;
        }

        public async Task<List<Chapter>> GetChaptersByPodcast(int idPodcast)
        {
            var listchapters = new List<Chapter>();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWSList>($"/api/Chapters/GetChaptersByPodcast/{idPodcast}");
                if (response.correct)
                {
                    listchapters = response.list;
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
            return listchapters;
        }

        public Task<bool> UpdateChapter(Chapter chapter)
        {
            throw new NotImplementedException();
        }

        class RespuestaWSList
        {
            public bool correct { get; set; }
            public int code { get; set; }
            public String message { get; set; }

            [JsonPropertyName("list")]
            public List<Chapter> list { get; set; }
        }
        class RespuestaWSobj
        {
            public bool correct { get; set; }
            public int code { get; set; }
            public String message { get; set; }
            public Chapter data { get; set; }
        }
    }
}
