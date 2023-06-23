using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Models;

public interface IUsersProvider
{
    public Task<List<User>> GetUsers();
    public Task<UserDTO> GetUserByEmail(string email);
    public Task<UserDTO> GetUserById(int userId);
    public Task<bool> UpdateUser(EditUserDTO user);
    public Task<bool> DeleteUser(UserDTO user);
    public Task<bool> UpgradeUserToUvCommiunity(Uvcomunity newUvcomunity);
    public Task<ProfileDTO> GetProfileById(int idUser);
    public Task<List<ProfileDTO>> GetProfiles();
    public Task<bool> UpgradeToUvComunity(NewUvComunityDTO newUvComunity);
    public Task<bool> AllreadyExistInstitutionalEmail(String email);

}