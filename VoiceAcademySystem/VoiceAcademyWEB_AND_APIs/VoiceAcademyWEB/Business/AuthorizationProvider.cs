using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;

namespace VoiceAcademyWEB.Business
{
    public class AuthorizationProvider : IAuthorizationProvider
    {
        HttpClient _cliente;
        TokenDTO _token;
        IMemoryCache _memoryCache;

        public AuthorizationProvider(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");

        }

        public async Task<bool> AddUser(UserDTO user)
        {
            var result = false;


            var json = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync("/api/Authorization/AddUser", json);
            response.EnsureSuccessStatusCode();
            var respuestaWS = await response.Content.ReadFromJsonAsync<RespuestaWSStandard>();
            if (respuestaWS.correct)
            {
                result = respuestaWS.correct;
            }
            else
            {
                throw new Exception(respuestaWS.message);
            }
            return result;
        }

        public async Task<bool> Login(LoginDTO loginInfo)
        {

            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(loginInfo), Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync("/api/Login/AuthorizeUser", json);
                response.EnsureSuccessStatusCode();
                var respuestaWS = await response.Content.ReadFromJsonAsync<RespuestaWS>();
                if (respuestaWS.correct)
                {
                    var token = respuestaWS.token;
                    _memoryCache.Set("Token", token);
                }
                else
                {
                    throw new Exception(respuestaWS.message);
                }
                return respuestaWS.correct;
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
        }
        class RespuestaWS
        {
            public bool correct { get; set; }
            public String message { get; set; }
            public int code { get; set; }
            public TokenDTO token { get; set; }
        }
    }

}
