using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Utility;

namespace VoiceAcademyWEB.Pages
{
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class PlayListViewModel : PageModel
    {

        public IPlayListProvider _playlist { get; set; }
        public IChaptersProvider _chapters { get; set; }


        private readonly IMemoryCache _memoryCache;

        [BindProperty]
        public PlayListDTO newPlayList { get; set; }
        [BindProperty]
        public int playlistId { get; set; }
        [BindProperty]
        public int chapterIdLike { get; set; }
        public PlayListViewModel(IPlayListProvider playlist, IMemoryCache memoryCache, IChaptersProvider chapters)
        {
            _playlist = playlist;
            _memoryCache = memoryCache;
            _chapters = chapters;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostList()
        {

            var list = new ChapterPlaylistDTO
            {
                IdChapter = chapterIdLike,
                IdPlaylist = playlistId
            };
            try
            {
                var result = await _playlist.DeleteChapterToPlayList(list);
                if (result)
                {
                    return RedirectToPage("PlayListView", new { id = playlistId });
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
