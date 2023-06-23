using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Utility;

namespace VoiceAcademyWEB.Pages
{
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class LogoutViewModel : PageModel
    {
        private readonly IMemoryCache _memoryCache;
        public LogoutViewModel(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IActionResult OnPost()
        {
            _memoryCache.Remove("Token");

            return RedirectToPage("/LoginView");
        }
    }
}
