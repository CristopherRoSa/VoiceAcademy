using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Models;
using VoiceAcademyWEB.Utility;

namespace VoiceAcademyWEB.Pages
{
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class UpdateUserModel : PageModel
    {

        public IUsersProvider _usersProvider { get; set; }
        private readonly IMemoryCache _memoryCache;
        [BindProperty]
        public EditUserDTO user { get; set; }
        [BindProperty]
        public IFormFile fileImage { get; set; }

        public UpdateUserModel(IUsersProvider users, IMemoryCache memoryCache)
        {
            _usersProvider = users;
            _memoryCache = memoryCache;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            TokenDTO token = _memoryCache.Get<TokenDTO>("Token");
            user.IdUser = token.idUser;
           
            var imagenListaFile = fileImage;
            if (imagenListaFile != null && imagenListaFile.Length > 0)
            {
                using (var stream = imagenListaFile.OpenReadStream())
                {
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    user.ImageUser = buffer;
                }
            }
            try
            {
                var result = await _usersProvider.UpdateUser(user);
                if (result)
                {
                    TempData["AlertType"] = "success";
                    TempData["AlertMessage"] = "Operación exitosa";
                    return RedirectToPage();
                }
                else
                {
                    TempData["AlertType"] = "error";
                    TempData["AlertMessage"] = "Se produjo un error al actualizar";
                    return Page();
                }
            }catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return Page();
            }
        }
    }
}
