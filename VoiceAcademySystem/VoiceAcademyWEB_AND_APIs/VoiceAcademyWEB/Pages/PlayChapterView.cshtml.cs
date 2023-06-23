using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.Models;
using VoiceAcademyWEB.Utility;

namespace VoiceAcademyWEB.Pages
{
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class PlayChapterViewModel : PageModel
    {
        public IChaptersProvider _chaptersProvider { get; set; }
        public PlayChapterViewModel(IChaptersProvider chaptersProvider, IMemoryCache memoryCache)
        {
            _chaptersProvider = chaptersProvider;
        }
        public void OnGet()
        {
        }
    }
}
