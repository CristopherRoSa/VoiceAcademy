namespace VoiceAcademyAPI.Business.Interface;

using Microsoft.AspNetCore.Mvc;
using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

public interface IUsersProvider
{
    public Task<List<User>> GetUsers();
    public Task<User> Login(LoginDTO login);
    public Task<User> GetUser(int id);
    public Task<int> AddUser(User newUser);
    public Task<int> UpdateUser(EditUserDTO userInfo);
    public Task<int> DeleteUser(int id);
    public Task<int> CreateLikesList(string email);
    public Task<ProfileDTO> GetProfileByID(int idUser);
    public Task<List<ProfileDTO>> GetProfiles();
    public bool SendVerificationToken(string email, int code);
    public Task<bool> AllreadyExistEmail(string email);
    public Task<bool> AllreadyExistInstitutionalEmail(string email);
    public Task<int> UpgradeToUvComunity(NewUvComunityDTO newUvComunity);
}