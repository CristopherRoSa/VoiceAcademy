using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;

namespace VoiceAcademyWEB.Pages
{
    public class RegisterViewModel : PageModel
    {
        public IAuthorizationProvider _users { get; set; }
        [BindProperty]
        public UserDTO user { get; set; }
        public RegisterViewModel(IAuthorizationProvider users)
        {
            _users = users;
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
                    user.Password = Utility.Utilities.Hash(user.Password);
                    var isRegister = await _users.AddUser(user);
                    if (isRegister)
                    {
                        return RedirectToPage("LoginView"); // Redirecciona a la página "LoginView"
                    }
                    else
                    {
                        ModelState.AddModelError("", "Error ocurrido al realizar el registro.");

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ninguno de los campos puede ser vacio, son necesarios para la creacion de una nueva cuenta");

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
            return Page();
        }

        private bool ValidateFields()
        {
            var result = false;
            if (!string.IsNullOrEmpty(user.Email) && !string.IsNullOrEmpty(user.Name) &&
                !string.IsNullOrEmpty(user.LastName) && !string.IsNullOrEmpty(user.Password) && user.Age > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
