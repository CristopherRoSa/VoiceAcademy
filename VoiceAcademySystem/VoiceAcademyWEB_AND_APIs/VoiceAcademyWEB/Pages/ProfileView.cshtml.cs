using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace VoiceAcademyWEB.Pages
{
    public class ProfileViewModel : PageModel
    {
        public IUsersProvider _usersProvider { get; set; }

        public ProfileViewModel(IUsersProvider usersProvider)
        {
            _usersProvider = usersProvider;
        }
        public void OnGet()
        {
        }
    }
}
