namespace VoiceAcademyAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using VoiceAcademyAPI.Business;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class PlayListsController : ControllerBase
{

    private readonly ILogger<PlayListsController> _logger;

    private readonly IPlayListsProvider _playListsProvider;

    public PlayListsController(IPlayListsProvider playListsProvider, ILogger<PlayListsController> logger)
    {
        _logger = logger;
        _playListsProvider = playListsProvider;
    }
    [HttpGet("GetPlayLists/{idUser}")]
    public async Task<IActionResult> GetPlayListsByUser(int idUser)
    {
        JsonResult result = new JsonResult(new { });
        try
        {

            List<Playlist> playLists = await _playListsProvider.GetPlaylistsByUser(idUser);
            if (playLists.Count > 0)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de PlayLists",
                    list = playLists
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 401,
                    message = "No se encontraron PlayLists",
                    list = new List<Playlist>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar PlayList: " + ex.Message,
                list = new List<Playlist>()
            });
        }
    }
    [HttpPost("AddPlayList")]
    public async Task<IActionResult> AddPlayList([FromBody] PlayListDTO newPlayList)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _playListsProvider.AddPlayList(newPlayList);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                                                  new
                                                  {
                                                      correct = true,
                                                      StatusCode = 200,
                                                      message = "PlayList agregado correctamente",
                                                  });
            }
            else
            {
                result = new JsonResult(
                                                  new
                                                  {
                                                      correct = false,
                                                      StatusCode = 401,
                                                      message = "Error al agregar el PlayList",
                                                  });
            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                                              new
                                              {
                                                  correct = false,
                                                  StatusCode = 500,
                                                  message = "Error al crear PlayList: " + ex.Message,
                                              });
        }
        return result;
    }

    [HttpPut("UpdatePlayList")]
    public async Task<IActionResult> UpdatePlayList([FromBody] PlayListDTO newPlayList)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _playListsProvider.UpdatePlayList(newPlayList);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                                        new
                                        {
                                            correct = true,
                                            StatusCode = 200,
                                            message = "PlayList actualizado correctamente",
                                        });
            }
            else
            {
                result = new JsonResult(
                                                                     new
                                                                     {
                                                                         correct = false,
                                                                         StatusCode = 401,
                                                                         message = "Error al actualizar el PlayList",
                                                                     });
            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                                                             new
                                                             {
                                                                 correct = false,
                                                                 StatusCode = 500,
                                                                 message = "Error al actualizar PlayList: " + ex.Message,
                                                             });
        }
        return result;
    }

    [HttpDelete("DeletePlayList/{idPlayList}")]
    public async Task<IActionResult> DeletePlayList(int idPlayList)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _playListsProvider.DeletePlayList(idPlayList);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                                                                                        new
                                                                                        {
                                                                                            correct = true,
                                                                                            StatusCode = 200,
                                                                                            message = "PlayList eliminado correctamente",
                                                                                        });
            }
            else
            {
                result = new JsonResult(
                                                                                        new
                                                                                        {
                                                                                            correct = false,
                                                                                            StatusCode = 401,
                                                                                            message = "Error al eliminar el PlayList",
                                                                                        });
            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                     new
                     {
                         correct = false,
                         StatusCode = 500,
                         message = "Error al eliminar PlayList: " + ex.Message,
                     });
        }
        return result;
    }
    [HttpGet("GetPlayListById/{idPlaylist}")]
    public async Task<IActionResult> GetPlayListsById(int idPlaylist)
    {
        try
        {

            Playlist playList = await _playListsProvider.GetPlaylistById(idPlaylist);
            if (playList != null)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de PlayLists",
                    list = playList
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 401,
                    message = "No se encontraron PlayLists",
                    list = new List<Playlist>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar PlayList: " + ex.Message,
                list = new List<Playlist>()
            });
        }
    }
    [HttpGet("GetLikeListById/{idLikelist}")]
    public async Task<IActionResult> GetLikeListById(int idLikelist)
    {
        try
        {

            LikeslistDTO likeList = await _playListsProvider.GetLikeListById(idLikelist);
            if (likeList != null)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de PlayLists",
                    list = likeList
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 401,
                    message = "No se encontraron PlayLists",
                    list = new List<Playlist>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar PlayList: " + ex.Message,
                list = new List<Playlist>()
            });
        }
    }
    [HttpPost("LikeChapter")]
    public async Task<IActionResult> LikeChapter([FromBody] LikeChapterDTO like)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _playListsProvider.AddChapterToLikeList(like);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                                                                     new
                                                                     {
                                                                         correct = true,
                                                                         StatusCode = 200,
                                                                         message = "Like agregado correctamente",
                                                                     });
            }
            else
            {
                result = new JsonResult(
                                                                     new
                                                                     {
                                                                         correct = false,
                                                                         StatusCode = 401,
                                                                         message = "Error al agregar el Like",
                                                                     });
            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                                                             new
                                                             {
                                                                 correct = false,
                                                                 StatusCode = 500,
                                                                 message = "Error al crear Like: " + ex.Message,
                                                             });
        }
        return result;
    }
    [HttpPost("AddChapterToPlayList")]
    public async Task<IActionResult> AddChapterToPlayList([FromBody] ChapterPlaylistDTO chapter)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _playListsProvider.AddChapterToPlayList(chapter);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                                                                                        new
                                                                                        {
                                                                                            correct = true,
                                                                                            StatusCode = 200,
                                                                                            message = "Capitulo agregado correctamente",
                                                                                        });
            }
            else
            {
                result = new JsonResult(
                                                                                        new
                                                                                        {
                                                                                            correct = false,
                                                                                            StatusCode = 401,
                                                                                            message = "Error al agregar el Capitulo",
                                                                                        });
            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                                                                            new
                                                                            {
                                                                                correct = false,
                                                                                StatusCode = 500,
                                                                                message = "Error al crear Capitulo: " + ex.Message,
                                                                            });
        }
        return result;
    }
    [HttpPut("DeleteLikeChapter")]
    public async Task<IActionResult> DeleteLikeChapter([FromBody] LikeChapterDTO like)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _playListsProvider.DeleteChapterToLikeList(like);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                                                                     new
                                                                     {
                                                                         correct = true,
                                                                         StatusCode = 200,
                                                                         message = "Like agregado correctamente",
                                                                     });
            }
            else
            {
                result = new JsonResult(
                                                                     new
                                                                     {
                                                                         correct = false,
                                                                         StatusCode = 401,
                                                                         message = "Error al agregar el Like",
                                                                     });
            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                                                             new
                                                             {
                                                                 correct = false,
                                                                 StatusCode = 500,
                                                                 message = "Error al crear Like: " + ex.Message,
                                                             });
        }
        return result;
    }
    [HttpPut("DeleteChapterToPlayList")]
    public async Task<IActionResult> DeleteChapterToPlayList([FromBody] ChapterPlaylistDTO chapter)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _playListsProvider.DeleteChapterToPlayList(chapter);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                                                                                        new
                                                                                        {
                                                                                            correct = true,
                                                                                            StatusCode = 200,
                                                                                            message = "Capitulo agregado correctamente",
                                                                                        });
            }
            else
            {
                result = new JsonResult(
                                                                                        new
                                                                                        {
                                                                                            correct = false,
                                                                                            StatusCode = 401,
                                                                                            message = "Error al agregar el Capitulo",
                                                                                        });
            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                                                                            new
                                                                            {
                                                                                correct = false,
                                                                                StatusCode = 500,
                                                                                message = "Error al crear Capitulo: " + ex.Message,
                                                                            });
        }
        return result;
    }

    [HttpPost("UnLikeChapter")]
    public async Task<IActionResult> UnLikeChapter([FromBody] LikeChapterDTO like)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _playListsProvider.DeleteChapterToLikeList(like);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                                                                     new
                                                                     {
                                                                         correct = true,
                                                                         StatusCode = 200,
                                                                         message = "Like eliminado correctamente",
                                                                     });
            }
            else
            {
                result = new JsonResult(
                                                                     new
                                                                     {
                                                                         correct = false,
                                                                         StatusCode = 401,
                                                                         message = "Error al eliminar el Like",
                                                                     });
            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                                                             new
                                                             {
                                                                 correct = false,
                                                                 StatusCode = 500,
                                                                 message = "Error al crear Like: " + ex.Message,
                                                             });
        }
        return result;
    }

    [HttpGet("GetUserIdPlaylistOwner/{idPlaylist}")]
    public async Task<IActionResult> IsPlaylistOwner(int idPlaylist)
    {
        var response = await _playListsProvider.GetUserIdPlaylistOwner(idPlaylist);
        if (response != ErrorCodes.ERROR)
        {
            return Ok(response);
        }
        else
        {
            return StatusCode(404, "No se encontro el correo el id del dueño de la playlist");
        }
    }
}