using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Models;
using VoiceAcademyWEB.Utility;

namespace VoiceAcademyWEB.Pages
{
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class PodcastViewModel : PageModel
    {
        public IPodcastChannelProvider _podcastProvider { get; set; }
        public IChaptersProvider _chaptersProvider { get; set; }
        public IPlayListProvider _playlistProvider { get; set; }
        public IUsersProvider _usersProvider { get; set; }
        private readonly IMemoryCache _memoryCache;

        [BindProperty]
        public int podcastId { get; set; }

        [BindProperty]
        public int chapterIdLike { get; set; }

        [BindProperty]
        public int chapterIdList { get; set; }
        [BindProperty]
        public int selectedPlaylistId { get; set; }

        public PodcastViewModel(IPodcastChannelProvider podcastProvider, IChaptersProvider chaptersProvider, IPlayListProvider playlistProvider, IMemoryCache memoryCache, IUsersProvider usersProvider)
        {
            _podcastProvider = podcastProvider;
            _chaptersProvider = chaptersProvider;
            _playlistProvider = playlistProvider;
            _memoryCache = memoryCache;
            _usersProvider = usersProvider;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLike()
        {
            TokenDTO token = _memoryCache.Get<TokenDTO>("Token");
            var like = new LikeChapterDTO
            {
                IdUser = token.idUser,
                IdChapter = chapterIdLike
            };
            try
            {
                var result = await _playlistProvider.LikeChapter(like);
                if (result)
                {
                    return RedirectToPage("PodcastView", new { id = podcastId });
                }
                else
                {
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }
        public async Task<IActionResult> OnPostList()
        {

            var list = new ChapterPlaylistDTO
            {
                IdChapter = chapterIdList,
                IdPlaylist = selectedPlaylistId
            };
            try
            {
                var result = await _playlistProvider.AddChapterToPlayList(list);
                if (result)
                {
                    return RedirectToPage("PodcastView", new { id = podcastId });
                }
                else
                {
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return Page();
            }
        }
    }
}
