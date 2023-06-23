namespace VoiceAcademyWEB.Business;
using VoiceAcademyWEB.Models;
using System.Globalization;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using VoiceAcademyWEB.DTOs;
using System.Collections.Generic;
using System.Net;

public class UsersProvider : IUsersProvider
{

    HttpClient _cliente;
    TokenDTO _token;
    IMemoryCache _memoryCache;

    public UsersProvider(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
    {
        var token = memoryCache.Get<TokenDTO>("Token");
        _token = token;
        _memoryCache = memoryCache;
        _cliente = httpClientFactory.CreateClient("ApiHttpClient");
        _cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {token.Token}");

    }
    public async Task<bool> UpdateUser(EditUserDTO user)
    {
        var result = false;
        var json = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

        try
        {

           
            var response = await _cliente.PutAsync("/api/User/UpdateUser", json);
            
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
        }
        catch (HttpRequestException)
        {
            throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador." + json);
        }
        return result;
    }
    public async Task<UserDTO> GetUserById(int idUser)
    {
        var infoUser = new UserDTO();
        try
        {
            var response = await _cliente.GetFromJsonAsync<RespuestaWSObject>($"/api/user/SearchUserById/{idUser}");
            if (response.correct)
            {
                infoUser = response.data;
            }
            else
            {
                throw new Exception(response.message);
            }
            return infoUser;
        }
        catch (HttpRequestException)
        {
            throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
        }
    }
    public Task<bool> DeleteUser(UserDTO user)
    {
        throw new NotImplementedException();
    }
    public async Task<List<User>> GetUsers()
    {
        var usersList = new List<User>();
        try
        {
            var response = await _cliente.GetFromJsonAsync<RespuestaWSUsers>("/api/user/GetUsers");
            if (response.correct)
            {
                usersList = response.list;
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
        return usersList;
    }
    public Task<bool> UpgradeUserToUvCommiunity(Uvcomunity newUvcomunity)
    {
        throw new NotImplementedException();
    }

    public Task<UserDTO> GetUserByEmail(string email)
    {
        throw new NotImplementedException();
    }
    public async Task<ProfileDTO> GetProfileById(int idUser)
    {
        var infoUser = new ProfileDTO();
        try
        {
            var response = await _cliente.GetFromJsonAsync<RespuestaWSProfile>($"/api/user/GetProfileById/{idUser}");
            if (response.correct)
            {
                infoUser = response.data;
            }
            else
            {
                throw new Exception(response.message);
            }
            return infoUser;
        }
        catch (HttpRequestException)
        {
            throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continua comuniquise con un administrador.");
        }
    }

    public async Task<List<ProfileDTO>> GetProfiles()
    {
        var usersList = new List<ProfileDTO>();
        try
        {
            var response = await _cliente.GetFromJsonAsync<RespuestaWSProfiles>("/api/user/GetProfiles");
            if (response.correct)
            {
                usersList = response.list;
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
        return usersList;
    }

    public async Task<bool> UpgradeToUvComunity(NewUvComunityDTO newUvComunity)
    {
        try
        {
            var json = new StringContent(JsonConvert.SerializeObject(newUvComunity), Encoding.UTF8, "application/json");
            var response = await _cliente.PostAsync($"/api/user/UpgradeToUvComunity", json);
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

    public async Task<bool> AllreadyExistInstitutionalEmail(string email)
    {
        try
        {
            var requestBody = new StringContent("", Encoding.UTF8, "application/json"); // No se envía ningún cuerpo en la solicitud POST

            var response = await _cliente.PostAsync($"/api/Authorization/AllreadyExistInstitutionalEmail?email={email}", requestBody);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                throw new Exception("Error en la solicitud");
            }
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException("Error de conexión con el servidor, inténtelo de nuevo, si el error continúa comuníquese con un administrador. " + ex);
        }
    }

    class RespuestaWSStandard
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
    }
    class RespuestaWSObject
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
        public UserDTO data { get; set; }
    }
    class RespuestaWSUsers
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
        public List<User> list { get; set; }
    }

    class RespuestaWSProfiles
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
        public List<ProfileDTO> list { get; set; }
    }
    class RespuestaWSProfile
    {
        public bool correct { get; set; }
        public int code { get; set; }
        public String message { get; set; }
        public ProfileDTO data { get; set; }
    }
}