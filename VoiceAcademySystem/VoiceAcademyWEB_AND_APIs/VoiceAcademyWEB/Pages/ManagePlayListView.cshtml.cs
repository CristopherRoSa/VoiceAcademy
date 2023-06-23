using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using VoiceAcademyWEB.Business;
using VoiceAcademyWEB.Business.Interface;
using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Utility;

namespace VoiceAcademyWEB.Pages
{
    [ServiceFilter(typeof(TokenAuthorizationFilter))]
    public class ManagePlayListViewModel : PageModel
    {
        public IPlayListProvider _playlist { get; set; }
        private readonly IMemoryCache _memoryCache;

        [BindProperty]
        public PlayListDTO newPlayList { get; set; }
        [BindProperty]
        public int playlistId { get; set; }
        [BindProperty]
        public int DeletePlaylistId { get; set; }
        [BindProperty]
        public IFormFile fileImage { get; set; }

        public ManagePlayListViewModel(IPlayListProvider playlist, IMemoryCache memoryCache)
        {
            _playlist = playlist;
            _memoryCache = memoryCache;
        }
        public async Task<IActionResult> OnPostAdd()
        {
            TokenDTO token = _memoryCache.Get<TokenDTO>("Token");
            newPlayList.IdUser = token.idUser;
            var imagenListaFile = fileImage;
            if (imagenListaFile != null && imagenListaFile.Length > 0)
            {
                using (var stream = imagenListaFile.OpenReadStream())
                {
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    newPlayList.Imagen = buffer;
                }
            }
            try
            {
                var result = await _playlist.AddPlayList(newPlayList);
                if (result)
                {
                    return RedirectToPage();
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
        public async Task<IActionResult> OnPostDelete()
        {
            try
            {
                var result = await _playlist.DeletePlayList(DeletePlaylistId);
                if (result)
                {
                    return RedirectToPage();
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
        public async Task<IActionResult> OnPostUpdate()
        {
            try
            {
                TokenDTO token = _memoryCache.Get<TokenDTO>("Token");
                newPlayList.IdUser = token.idUser;
                newPlayList.IdPlayList = playlistId;
                var imagenListaFile = fileImage;
                if (imagenListaFile != null && imagenListaFile.Length > 0)
                {
                    using (var stream = imagenListaFile.OpenReadStream())
                    {
                        var buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        newPlayList.Imagen = buffer;
                    }
                }
                try
                {
                    var result = await _playlist.UpdatePlayList(newPlayList);
                    if (result)
                    {
                        return RedirectToPage();
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
            catch (Exception ex)
            {
                Console.WriteLine("Error al procesar la imagen: " + ex.Message);
                return Page();
            }
        }

        public IActionResult OnPostSee()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //crear

            return RedirectToAction("PlayListView", new { id = playlistId });
        }


    }
}
