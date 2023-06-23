using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.Utility;

namespace VoiceAcademyWEB.Pages
{
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class SearchPodcastViewModel : PageModel
    {
        public IPodcastChannelProvider _podcastProvider { get; set; }

        [BindProperty]
        public int userId { get; set; }

        public SearchPodcastViewModel(IPodcastChannelProvider podcastProvider)
        {
            _podcastProvider = podcastProvider;

        }
        public void OnGet()
        {
        }
    }
}
