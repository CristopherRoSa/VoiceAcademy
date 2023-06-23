using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.Utility;

namespace VoiceAcademyWEB.Pages
{
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class SearchProfileViewModel : PageModel
    {
        public IUsersProvider _usersProvider { get; set; }
        public IDegreeProvider _degreeProvider { get; set; }
        [BindProperty]
        public int userId { get; set; }

        public SearchProfileViewModel(IUsersProvider usersProvider, IDegreeProvider degreeProvider)
        {
            _usersProvider = usersProvider;
            _degreeProvider = degreeProvider;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Console.WriteLine(userId);
            return RedirectToPage("/ProfileView", new { id = userId });
        }


    }
}
