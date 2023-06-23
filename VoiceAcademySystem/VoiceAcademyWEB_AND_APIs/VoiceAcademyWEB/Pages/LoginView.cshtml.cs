using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;

namespace VoiceAcademyWEB.Pages
{
    public class LoginViewModel : PageModel
    {

        public IAuthorizationProvider _users { get; set; }
        private readonly IMemoryCache _memoryCache;

        [BindProperty]
        public LoginDTO login { get; set; }

        public LoginViewModel(IAuthorizationProvider users, IMemoryCache memoryCache)
        {
            _users = users;
            _memoryCache = memoryCache;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (ValidateFields())
                {
                    login.Password = Utility.Utilities.Hash(login.Password);

                    var isAuthenticated = await _users.Login(login);

                    if (isAuthenticated)
                    {
                        TokenDTO token = _memoryCache.Get<TokenDTO>("Token");
                        return RedirectToPage("Privacy"); // Redirecciona a la página "Privacy"
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuario o contraseña invalida.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ninguno de los campos puede ser vacio, son necesarios para la validacion de credenciales");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();

            }
        }

        private bool ValidateFields()
        {
            var result = false;
            if (!string.IsNullOrEmpty(login.User) && !string.IsNullOrEmpty(login.Password))
            {
                result = true;
            }
            return result;
        }
    }
}
