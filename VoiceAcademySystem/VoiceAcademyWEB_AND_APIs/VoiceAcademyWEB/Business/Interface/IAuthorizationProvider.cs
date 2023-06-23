using VoiceAcademyWEB.DTOs;

namespace VoiceAcademyWEB.Business.Interface
{
    public interface IAuthorizationProvider
    {
        public Task<bool> Login(LoginDTO loginInfo);
        public Task<bool> AddUser(UserDTO user);
    }
}
