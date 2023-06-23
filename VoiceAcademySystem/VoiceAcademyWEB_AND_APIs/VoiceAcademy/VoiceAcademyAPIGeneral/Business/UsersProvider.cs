namespace VoiceAcademyAPI.Business;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPI.Utility;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class UsersProvider : IUsersProvider
{
    private readonly VoiceacademydbContext _context;

    public UsersProvider(VoiceacademydbContext context)
    {
        string culture = "es-MX";
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
        _context = context;

    }

    public async Task<List<User>> GetUsers()
    {
        bool canConnect = await _context.Database.CanConnectAsync();

        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            return await _context.Users.OrderBy(c => c.IdUser).ToListAsync();
        }
    }

    public async Task<int> AddUser(User newUser)
    {
        var result = ErrorCodes.ERROR;
        bool canConnect = await _context.Database.CanConnectAsync();

        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            var user = await _context.Users.Where(x => x.Email == newUser.Email).FirstOrDefaultAsync();
            if (user == null)
            {
                await _context.Users.AddAsync(newUser);
                result = await _context.SaveChangesAsync();
            }
        }
        return result;
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

    public async Task<User> GetUser(int id)
    {

        bool canConnect = await _context.Database.CanConnectAsync();

        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
        }


    }

    public async Task<int> DeleteUser(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateUser(EditUserDTO userInfo)
    {
        var result = ErrorCodes.ERROR;
        bool canConnect = await _context.Database.CanConnectAsync();

        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            var user = await _context.Users.Where(x => x.IdUser == userInfo.IdUser).FirstOrDefaultAsync();
            if (user != null)
            {
                user.Age = userInfo.Age;
                user.ImageUser = userInfo.ImageUser;
                user.Name = userInfo.Name;
                user.LastName = userInfo.LastName;
                result = _context.SaveChanges();
            }
        }
        return result;
    }

    public async Task<int> CreateLikesList(string email)
    {
        var result = ErrorCodes.ERROR;
        bool canConnect = await _context.Database.CanConnectAsync();

        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                var list = new Likeslist
                {
                    UserIdUser = user.IdUser,
                };
                _context.Likeslists.Add(list);
                result = _context.SaveChanges();
            }
        }
        return result;
    }

    public async Task<ProfileDTO> GetProfileByID(int idUser)
    {
        bool canConnect = await _context.Database.CanConnectAsync();
        var profile = new ProfileDTO();
        var uvcomunityInfo = new Uvcomunity();
        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            var user = await _context.Users
                .Include(u => u.Likeslists)
                .Include(u => u.Uvcomunity)
                .Where(u => u.IdUser == idUser)
                .FirstOrDefaultAsync();

            if (user.Uvcomunity != null)
            {
                uvcomunityInfo = await _context.Uvcomunities
                                            .Include(u => u.Podcastchannels)
                                            .Include(p => p.Playlists)
                                            .Where(u => u.UserIdUser == idUser)
                                            .FirstOrDefaultAsync();
            }
            if (user != null)
            {
                profile.IdUser = user.IdUser;
                profile.FullName = user.Name + " " + user.LastName;
                profile.Role = user.Rol;
                profile.Email = user.Email;
                profile.likesList = _context.Likeslists.Where(l => l.UserIdUser == idUser).FirstOrDefault();
            }
            if (uvcomunityInfo != null)
            {
                profile.podcastchannels = (List<Podcastchannel>?)uvcomunityInfo.Podcastchannels;
                profile.playLists = (List<Playlist>?)uvcomunityInfo.Playlists;
                profile.uvcomunity = uvcomunityInfo;
            }
            else
            {
                throw new Exception("No se encontró el usuario con el ID especificado.");
            }
        }
        return profile;
    }

    public async Task<List<ProfileDTO>> GetProfiles()
    {
        bool canConnect = await _context.Database.CanConnectAsync();
        var profiles = new List<ProfileDTO>();
        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            var user = await _context.Users
                .Include(u => u.Likeslists)
                .Include(u => u.Uvcomunity)
                .Where(u => u.Rol == "uvcomunity" || u.Rol == "userGeneral").ToListAsync();

            foreach (var item in user)
            {
                var profile = new ProfileDTO();

                profile.IdUser = item.IdUser;
                profile.FullName = item.Name + " " + item.LastName;
                profile.Role = item.Rol;
                profile.ImagenUser = item.ImageUser;
                profile.Email = item.Email;
                profile.likesList = _context.Likeslists.Where(l => l.UserIdUser == item.IdUser).FirstOrDefault();

                if (item.Rol.Equals("uvcomunity"))
                {
                    profile.uvcomunity = item.Uvcomunity;
                }
                else
                {
                    profile.uvcomunity = new Uvcomunity();

                }
                profiles.Add(profile);
            }
        }
        return profiles;
    }

    public bool SendVerificationToken(string email, int code)
    {
        return Utilities.SendToken(email, code);
    }

    public async Task<bool> AllreadyExistEmail(string email)
    {
        var result = false;
        bool canConnect = _context.Database.CanConnect();

        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user != null)
            {
                result = true;
            }
        }
        return result;
    }

    public async Task<int> UpgradeToUvComunity(NewUvComunityDTO newUvComunity)
    {
        var result = ErrorCodes.ERROR;
        bool canConnect = _context.Database.CanConnect();

        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            var user = await _context.Users.Where(x => x.IdUser == newUvComunity.UserIdUser).FirstOrDefaultAsync();
            user.Rol = "uvcomunity";
            var res = _context.SaveChanges();
            if (res == 1)
            {
                var newUv = new Uvcomunity()
                {
                    UserIdUser = newUvComunity.UserIdUser,
                    InstitutionalEmail = newUvComunity.InstitutionalEmail,
                    StudentNumber = newUvComunity.StudentNumber,
                    DegreeIdDegree = newUvComunity.IdDegree,
                };
                await _context.Uvcomunities.AddAsync(newUv);
                result = _context.SaveChanges();
            }
        }
        return result;
    }

    public async Task<bool> AllreadyExistInstitutionalEmail(string email)
    {
        var result = false;
        bool canConnect = _context.Database.CanConnect();

        if (!canConnect)
        {
            throw new Exception("No se pudo establecer conexión con la base de datos.");
        }
        else
        {
            var user = await _context.Uvcomunities.Where(x => x.InstitutionalEmail == email).FirstOrDefaultAsync();
            if (user != null)
            {
                result = true;
            }
        }
        return result;
    }
}