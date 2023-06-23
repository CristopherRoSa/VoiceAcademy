using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;

namespace VoiceAcademyWEB.Pages
{
    public class LikeListViewModel : PageModel
    {
        public int PlaylistId { get; set; }
        public IPlayListProvider _playlist { get; set; }
        public IChaptersProvider _chapters { get; set; }

        private readonly IMemoryCache _memoryCache;

        [BindProperty]
        public PlayListDTO newPlayList { get; set; }
        [BindProperty]
        public int playlistId { get; set; }
        [BindProperty]
        public int chapterIdLike { get; set; }
        public LikeListViewModel(IPlayListProvider playlist, IMemoryCache memoryCache, IChaptersProvider chapters)
        {
            _playlist = playlist;
            _memoryCache = memoryCache;
            _chapters = chapters;
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
                var result = await _playlist.DeleteLikeChapter(like);
                if (result)
                {
                    return RedirectToPage("LikeListView", new { id = playlistId });
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
