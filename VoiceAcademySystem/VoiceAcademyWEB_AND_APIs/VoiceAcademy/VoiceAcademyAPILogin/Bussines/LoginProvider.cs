using System.Globalization;
using Microsoft.EntityFrameworkCore;
using VoiceAcademyAPILogin.Bussines.Interface;
using VoiceAcademyAPILogin.DTOs;
using VoiceAcademyAPILogin.Models;

namespace VoiceAcademyAPILogin.Bussines
{
    public class LoginProvider : ILoginProvider
    {
        private readonly VoiceacademydbContext _context;

        public LoginProvider(VoiceacademydbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }
        public async Task<User> Login(LoginDTO login)
        {
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                return await _context.Users.Where(x => x.Email == login.User).FirstOrDefaultAsync();
            }
        }
    }
}
