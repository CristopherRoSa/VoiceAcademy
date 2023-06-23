
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Models;
using static VoiceAcademyWEB.Business.PlayListProvider;

namespace VoiceAcademyWEB.Business
{

    public class PlayListProvider : IPlayListProvider
    {

        HttpClient _cliente;
        TokenDTO _token;

        public PlayListProvider(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            var token = memoryCache.Get<TokenDTO>("Token");
            _token = token;
            _cliente = httpClientFactory.CreateClient("ApiHttpClient");
            _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");

        }

        public async Task<PlayList> GetPlayListById(int playListId)
        {
            var listaCursos = new PlayList();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWS>($"/api/PlayLists/GetPlayListById/{playListId}");
                if (response.correct)
                {
                    listaCursos = response.list;
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
            return listaCursos;
        }

        public async Task<List<PlayList>> GetPlayListsByUser(int userId)
        {
            var listaCursos = new List<PlayList>();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWSLista>($"/api/PlayLists/GetPlayLists/{userId}");
                if (response.correct)
                {
                    listaCursos = response.list;
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
            return listaCursos;
        }
        public async Task<bool> UpdatePlayList(PlayListDTO newPlayList)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(newPlayList), Encoding.UTF8, "application/json");
                var response = await _cliente.PutAsync($"/api/PlayLists/UpdatePlayList", json);
                var respuestaWSS = await response.Content.ReadFromJsonAsync<RespuestaWSStandard>();
                if (respuestaWSS.correct)
                {
                    return respuestaWSS.correct;
                }
                else
                {
                    throw new Exception(respuestaWSS.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
        }
        public async Task<bool> DeletePlayList(int idPlayList)
        {
            try
            {
                var response = await _cliente.DeleteAsync($"/api/PlayLists/DeletePlayList/{idPlayList}");
                var respuestaWSS = await response.Content.ReadFromJsonAsync<RespuestaWSStandard>();
                if (respuestaWSS.correct)
                {
                    return respuestaWSS.correct;
                }
                else
                {
                    throw new Exception(respuestaWSS.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
        }
        public async Task<bool> AddPlayList(PlayListDTO newPlayList)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(newPlayList), Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync($"/api/PlayLists/AddPlayList", json);
                var respuestaWSS = await response.Content.ReadFromJsonAsync<RespuestaWSStandard>();
                if (respuestaWSS.correct)
                {
                    return respuestaWSS.correct;
                }
                else
                {
                    throw new Exception(respuestaWSS.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
        }

        public async Task<LikeslistDTO> GetLikesList(int idLikelist)
        {
            var list = new LikeslistDTO();
            try
            {
                var response = await _cliente.GetFromJsonAsync<RespuestaWSLike>($"/api/PlayLists/GetLikeListById/{idLikelist}");
                if (response.correct)
                {
                    list = response.list;
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
            return list;
        }

        public async Task<bool> LikeChapter(LikeChapterDTO like)
        {

            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(like), Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync($"/api/PlayLists/LikeChapter", json);
                var respuestaWSS = await response.Content.ReadFromJsonAsync<RespuestaWSStandard>();
                if (respuestaWSS.correct)
                {
                    return respuestaWSS.correct;
                }
                else
                {
                    throw new Exception(respuestaWSS.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
        }

        public async Task<bool> AddChapterToPlayList(ChapterPlaylistDTO list)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(list), Encoding.UTF8, "application/json");
                var response = await _cliente.PostAsync($"/api/PlayLists/AddChapterToPlayList", json);
                var respuestaWSS = await response.Content.ReadFromJsonAsync<RespuestaWSStandard>();
                if (respuestaWSS.correct)
                {
                    return respuestaWSS.correct;
                }
                else
                {
                    throw new Exception(respuestaWSS.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
        }

        public async Task<bool> DeleteLikeChapter(LikeChapterDTO like)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(like), Encoding.UTF8, "application/json");
                var response = await _cliente.PutAsync($"/api/PlayLists/DeleteLikeChapter", json);
                var respuestaWSS = await response.Content.ReadFromJsonAsync<RespuestaWSStandard>();
                if (respuestaWSS.correct)
                {
                    return respuestaWSS.correct;
                }
                else
                {
                    throw new Exception(respuestaWSS.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
        }
        public async Task<bool> DeleteChapterToPlayList(ChapterPlaylistDTO list)
        {
            try
            {
                var json = new StringContent(JsonConvert.SerializeObject(list), Encoding.UTF8, "application/json");
                var response = await _cliente.PutAsync($"/api/PlayLists/DeleteChapterToPlayList", json);
                var respuestaWSS = await response.Content.ReadFromJsonAsync<RespuestaWSStandard>();
                if (respuestaWSS.correct)
                {
                    return respuestaWSS.correct;
                }
                else
                {
                    throw new Exception(respuestaWSS.message);
                }
            }
            catch (HttpRequestException)
            {
                throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
            }
        }
        public async Task<int> GetUserIdPlaylistOwner(int idPlaylist)
        {
            try
            {
                HttpResponseMessage response = await _cliente.GetAsync($"/api/PlayLists/GetUserIdPlaylistOwner/{idPlaylist}");

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return int.Parse(responseBody);
                }
                else
                {
                    Console.WriteLine("Error en la solicitud: " + response.StatusCode);
                    return -1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la solicitud: " + ex.Message);
                return -1;
            }
        }
    }

    class RespuestaWSLista
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }

        [JsonPropertyName("list")]
        public List<PlayList> list { get; set; }
    }

    class RespuestaWS
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }

        [JsonPropertyName("list")]
        public PlayList list { get; set; }
    }
    class RespuestaWSStandard
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
    }
    class RespuestaWSLike
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }

        [JsonPropertyName("list")]
        public LikeslistDTO list { get; set; }
    }
}
