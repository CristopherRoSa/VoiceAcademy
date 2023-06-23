using VoiceAcademyAPILogin.DTOs;
using VoiceAcademyAPILogin.Models;

namespace VoiceAcademyAPILogin.Bussines.Interface
{
    public interface ILoginProvider
    {
        public Task<User> Login(LoginDTO login);
    }
}
