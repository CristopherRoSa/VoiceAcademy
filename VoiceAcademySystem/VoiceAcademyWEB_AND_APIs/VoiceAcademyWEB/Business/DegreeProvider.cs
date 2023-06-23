

using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Models;

namespace VoiceAcademyWEB.Business
{
    public class DegreeProvider : IDegreeProvider
    {
        HttpClient _cliente;
        TokenDTO _token;
        IMemoryCache _memoryCache;

        public DegreeProvider(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            var token = memoryCache.Get<TokenDTO>("Token");
            _token = token;
            _memoryCache = memoryCache;
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");

        }
        public async Task<List<DegreeDTO>> GetDegrees()
        {
            var degreeList = new List<DegreeDTO>();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWSGrados>("/api/Degree/GetDegrees");
                if (response.correct)
                {
                    degreeList = response.list;
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
            return degreeList;
        }
    }
    class RespuestaWSGrados
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
        public List<DegreeDTO> list { get; set; }
    }
}
