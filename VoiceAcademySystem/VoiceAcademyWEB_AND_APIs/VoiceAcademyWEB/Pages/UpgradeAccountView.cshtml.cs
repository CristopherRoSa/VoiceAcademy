using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;

namespace VoiceAcademyWEB.Pages
{
    public class UpgradeAccountViewModel : PageModel
    {
        public IUsersProvider _usersProvider { get; set; }
        public IDegreeProvider _degreeProvider { get; set; }
        public IMemoryCache _memoryCache { get; set; }

        public UpgradeAccountViewModel(IUsersProvider usersProvider, IDegreeProvider degreeProvider, IMemoryCache memoryCache)
        {
            _usersProvider = usersProvider;
            _degreeProvider = degreeProvider;
            _memoryCache = memoryCache;
        }

        public async Task<IActionResult> OnPostUpgradeAsync(string carrera, string matricula, string correo, int? id)
        {
            TokenDTO token = _memoryCache.Get<TokenDTO>("Token");
            if (!string.IsNullOrEmpty(carrera) && !string.IsNullOrEmpty(matricula) && !string.IsNullOrEmpty(correo))
            {
                bool existEmail = await _usersProvider.AllreadyExistInstitutionalEmail(correo);
                if (!existEmail)
                {
                    var uvcomunity = new NewUvComunityDTO();
                    uvcomunity.StudentNumber = matricula;
                    uvcomunity.InstitutionalEmail = correo;
                    uvcomunity.IdDegree = int.Parse(carrera);
                    uvcomunity.UserIdUser = token.idUser;
                    // Redireccionar a una página de éxito o mostrar un mensaje de confirmación
                    bool upgradeSuccessful = await _usersProvider.UpgradeToUvComunity(uvcomunity);
                    if (upgradeSuccessful)
                    {
                        token = new TokenDTO();
                        return RedirectToPage("/LoginVIew");
                    }
                    else
                    {
                        ViewData["ErrorMessage"] = "Hubo un error al realizar la actualización a UvComunity.";
                        return RedirectToPage("/UpdateUser");
                    }
                }
                ViewData["ErrorMessage"] = "El correo isntitucional porporcionado ya se encuentra registrado, \n Por favor intenta con otro.";
                return RedirectToPage("/UpdateUser");
            }

            return RedirectToPage("/UpdateUser");
        }
    }
}
