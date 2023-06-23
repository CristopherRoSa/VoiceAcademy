namespace VoiceAcademyAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using VoiceAcademyAPI.Business;
using VoiceAcademyAPI.Business.Interface;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using VoiceAcademyAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using VoiceAcademyAPIGeneral.Models;
using System.ComponentModel;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPI.Utility;

[ApiController]
[Authorize]
[Route("api/[controller]")]

public class ChaptersController : ControllerBase
{

    private readonly ILogger<ChaptersController> _logger;
    private IChaptersProvider _requesrProvider;

    public ChaptersController(IChaptersProvider requestProvider, ILogger<ChaptersController> logger)
    {
        _logger = logger;
        _requesrProvider = requestProvider;
    }

    [HttpGet("GetChaptersByPlaylist/{idPlaylist}")]
    public async Task<IActionResult> GetChaptersByPlaylist(int idPlaylist)
    {
        try
        {

            List<Chapter> chapters = await _requesrProvider.GetChaptersByPlaylist(idPlaylist);
            if (chapters.Count > 0)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de Capitulos",
                    list = chapters
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 500,
                    message = "No se encontraron Capitulos",
                    list = new List<Chapter>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar Capitulos: " + ex.Message,
                list = new List<Chapter>()
            });
        }
    }
    [HttpGet("GetChaptersByPodcast/{idPodcast}")]
    public async Task<IActionResult> GetChapterByPodcast(int idPodcast)
    {
        try
        {
            List<Chapter> chapters = await _requesrProvider.GetChaptersByPodcast(idPodcast);
            if (chapters.Count > 0)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de Capitulos",
                    list = chapters
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 500,
                    message = "No se encontraron Capitulos",
                    list = new List<Chapter>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar Capitulos: " + ex.Message,
                list = new List<Chapter>()
            });
        }
    }

    [HttpGet("GetChapterById/{idChapter}")]
    public async Task<IActionResult> GetChapterById(int idChapter)
    {
        try
        {

            Chapter chapter = await _requesrProvider.GetChapterById(idChapter);
            if (chapter != null)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Capitulo",
                    data = chapter
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 500,
                    message = "No se encontro Capitulo",
                    data = new Chapter()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar Capitulo: " + ex.Message,
                data = new Chapter()
            });
        }
    }
    [HttpGet("GetLikedChaptersByPodcastId/{idPodcast}/{idUser}")]
    public async Task<IActionResult> GetLikedChaptersByPodcastId(int idPodcast, int idUser)
    {
        try
        {

            List<Chapter> chapters = await _requesrProvider.GetLikedChaptersByPodcastId(idPodcast, idUser);
            if (chapters.Count > 0)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de Capitulos",
                    list = chapters
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 500,
                    message = "No se encontraron Capitulos",
                    list = new List<Chapter>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar Capitulos: " + ex.Message,
                list = new List<Chapter>()
            });
        }
    }

    [HttpGet("GetLikedChaptersByUserId/{idUser}")]
    public async Task<IActionResult> GetLikedChaptersByUserId(int idUser)
    {
        try
        {

            List<Chapter> chapters = await _requesrProvider.GetLikedChaptersByUserId(idUser);
            if (chapters.Count > 0)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de Capitulos",
                    list = chapters
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 500,
                    message = "No se encontraron Capitulos",
                    list = new List<Chapter>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar Capitulos: " + ex.Message,
                list = new List<Chapter>()
            });
        }
    }

    static string[] Scopes = { DriveService.Scope.Drive };
    static string ApplicationName = "Nombre de tu aplicación";

    [HttpPost("AddChapter")]
    public async Task<IActionResult> AddChapter(ChapterDTO chapterDTO)
    {
        JsonResult result = new JsonResult(new { });
        try
        {

            int response = await _requesrProvider.AddChapter(chapterDTO);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                    new
                    {
                        correct = true,
                        code = 200,
                        message = "Capítulo agregado correctamente",
                    });

            }
            else
            {
                result = new JsonResult(
                    new
                    {
                        correct = false,
                        code = 401,
                        message = "Error al agregar Capítulo, " + response,
                    });

            }
        }
        catch (Exception ex)
        {
            result = new JsonResult(
                new
                {
                    correct = false,
                    code = 500,
                    message = "Error al crear un nuevo Capítulo: " + ex.Message,
                });
        }
        return result;
    }


}