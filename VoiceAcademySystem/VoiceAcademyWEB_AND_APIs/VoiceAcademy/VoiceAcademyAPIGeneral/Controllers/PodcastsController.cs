namespace VoiceAcademyAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using VoiceAcademyAPI.Business;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPI.Utility;
using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using VoiceAcademyAPIGeneral.Models;
using VoiceAcademyAPIGeneral.DTOs;

[ApiController]
[Authorize]
[Route("api/[controller]")]

public class PodcastsController : ControllerBase
{

    private readonly ILogger<PodcastsController> _logger;
    private IPodcastProvider _requesrProvider;

    public PodcastsController(IPodcastProvider requestProvider, ILogger<PodcastsController> logger)
    {
        _logger = logger;
        _requesrProvider = requestProvider;
    }
    [HttpPost("AddPodcast")]
    public async Task<IActionResult> AddPodcast([FromBody] PodcastDTO newPodcast)
    {
        JsonResult result = new JsonResult(new { });
        try
        {

            int response = await _requesrProvider.AddPodcast(newPodcast);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                    new
                    {
                        correct = true,
                        code = 200,
                        message = "Podcast agregado correctamente",
                    });

            }
            else
            {
                result = new JsonResult(
                    new
                    {
                        correct = false,
                        code = 401,
                        message = "Error al agregar Podcast, " + response,
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
                    message = "Error al crear un nuevo Podcast: " + ex.Message,
                });
        }
        return result;
    }
    [HttpGet("GetPodcasts")]
    public async Task<IActionResult> GetPodcasts()
    {
        try
        {

            List<Podcastchannel> podcasts = await _requesrProvider.GetPodcasts();
            if (podcasts != null)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de Podcasts",
                    list = podcasts
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 500,
                    message = "No se encontraron Podcasts",
                    list = new List<Podcastchannel>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar Podcast: " + ex.Message,
                list = new List<Podcastchannel>()
            });
        }
    }

    [HttpGet("GetPodcastsByUserId/{idUser}")]
    public async Task<IActionResult> GetPodcastsByUserId(int idUser)
    {
        try
        {

            List<Podcastchannel> podcasts = await _requesrProvider.GetPodcastByUserId(idUser);
            if (podcasts.Count > 0)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Lista de Podcasts",
                    list = podcasts
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 500,
                    message = "No se encontraron Podcasts del usuario con id: " + idUser,
                    list = new List<Podcastchannel>()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar podcast Podcast: " + ex.Message,
                list = new List<Podcastchannel>()
            });
        }
    }

    [HttpDelete("DeletePodcast")]
    public async Task<IActionResult> DeletePodcast(int idPodcast)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _requesrProvider.DeletePodcast(idPodcast);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                    new
                    {
                        correct = true,
                        StatusCode = 200,
                        message = "Podcast eliminado correctamente",
                    });

            }
            else
            {
                result = new JsonResult(
                    new
                    {
                        correct = false,
                        StatusCode = 500,
                        message = "Error al eliminar Podcast",
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
                         message = "Error al eliminar Podcast: " + ex.Message,
                     });
        }
        return result;
    }
    [HttpPut("UpdatePodcast")]
    public async Task<IActionResult> UpdatePodcast([FromBody] NewPodcastDTO podcastDTO, int idPodcast)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _requesrProvider.UpdatePodcast(podcastDTO, idPodcast);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                    new
                    {
                        correct = true,
                        StatusCode = 200,
                        message = "Podcast actualizado correctamente",
                    });

            }
            else
            {
                result = new JsonResult(
                    new
                    {
                        correct = false,
                        StatusCode = 500,
                        message = "Error al actualizar Podcast",
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
                    message = "Error al actualizar Podcast: " + ex.Message,
                });

        }
        return result;
    }
    [HttpPut("ChangePodcastVisivility")]
    public async Task<IActionResult> ChangePodcastVisivility(int idPodcast)
    {
        JsonResult result = new JsonResult(new { });
        try
        {
            int response = await _requesrProvider.ChangePodcastVisibility(idPodcast);
            if (response == ErrorCodes.SUCCESS)
            {
                result = new JsonResult(
                    new
                    {
                        correct = true,
                        StatusCode = 200,
                        message = "Visibilidad del Podcast actualizada correctamente",
                    });

            }
            else
            {
                result = new JsonResult(
                    new
                    {
                        correct = false,
                        StatusCode = 500,
                        message = "Error al actualizar visibilidad del Podcast",
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
                    message = "Error al actualizar Podcast: " + ex.Message,
                });

        }
        return result;
    }
    [HttpGet("GetPodcastById/{idPodcast}")]
    public async Task<IActionResult> GetPodcastById(int idPodcast)
    {
        try
        {
            Podcastchannel podcast = await _requesrProvider.GetPodcastById(idPodcast);
            if (podcast != null)
            {
                return new JsonResult(new
                {
                    correct = true,
                    code = 200,
                    message = "Podcast",
                    data = podcast
                });
            }
            else
            {
                return new JsonResult(new
                {
                    correct = false,
                    code = 500,
                    message = "No se encontró el Podcast con id: " + idPodcast,
                    data = new Podcastchannel()
                });
            }
        }
        catch (Exception ex)
        {
            return new JsonResult(new
            {
                correct = false,
                code = 500,
                message = "Error al buscar Podcast: " + ex.Message,
                data = new Podcastchannel()
            });
        }
    }
}