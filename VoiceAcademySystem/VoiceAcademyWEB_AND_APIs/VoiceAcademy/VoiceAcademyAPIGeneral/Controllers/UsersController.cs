namespace VoiceAcademyAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPI.Utility;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private IUsersProvider _usersProvider;

    public UserController(IUsersProvider usersProvider, ILogger<UserController> logger)
    {
        _logger = logger;
        _usersProvider = usersProvider;
    }

    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers()
    {

        try
        {
            var list = await _usersProvider.GetUsers();
            if (list != null)
                return new JsonResult(
                                       new
                                       {
                                           correct = true,
                                           code = 200,
                                           message = "",
                                           list = list
                                       });
            else
                return new JsonResult(
                                       new
                                       {
                                           correct = false,
                                           code = 401,
                                           message = "No existen usuarios",
                                           list = new List<User>()
                                       });
        }
        catch (Exception ex)
        {
            return new JsonResult(
                                                  new
                                                  {
                                                      correct = false,
                                                      code = 500,
                                                      message = "Error al obtener los usuarios: " + ex.Message,
                                                      list = new List<User>()
                                                  });
        }

    }

    [HttpGet("SearchUserById/{idUser}")]
    public async Task<IActionResult> GetUser(int idUser)
    {
        try
        {
            var user = await _usersProvider.GetUser(idUser);
            if (user != null)
                return new JsonResult(
                    new
                    {
                        correct = true,
                        code = 200,
                        message = "",
                        data = user
                    });
            else
                return new JsonResult(
                    new
                    {
                        correct = false,
                        code = 401,
                        message = "Ocurrio un error al buscar los usuarios",
                        list = new User()
                    });
        }
        catch (Exception ex)
        {
            return new JsonResult(
                                   new
                                   {
                                       correct = false,
                                       code = 500,
                                       message = "Error al obtener el usuario: " + ex.Message,
                                       list = new User()
                                   });
        }

    }
    [HttpPut("UpdateUser")]
    public async Task<IActionResult> UpdateUser(EditUserDTO user)
    {
        try
        {
            var result = await _usersProvider.UpdateUser(user);
            if (result != ErrorCodes.ERROR)
                return new JsonResult(
                                   new
                                   {
                                       correct = true,
                                       code = 200,
                                       message = "",
                                   });
            else
                return new JsonResult(
                                   new
                                   {
                                       correct = false,
                                       code = 401,
                                       mensaje = "No existe un usuario con ese ID, favor de verificar",
                                   });
        }
        catch (Exception ex)
        {
            return new JsonResult(
                                 new
                                 {
                                     correct = false,
                                     code = 500,
                                     message = "Error al obtener el usuario: " + ex.Message,
                                 });
        }

    }

    [HttpGet("GetProfileById/{idUser}")]
    public async Task<IActionResult> GetProfile(int idUser)
    {
        try
        {
            var user = await _usersProvider.GetProfileByID(idUser);
            if (user != null)
                return new JsonResult(
                                       new
                                       {
                                           correct = true,
                                           code = 200,
                                           message = "",
                                           data = user
                                       });
            else
                return new JsonResult(
                                       new
                                       {
                                           correct = false,
                                           code = 401,
                                           message = "Ocurrio un error al buscar los usuarios",
                                           data = new ProfileDTO()
                                       });
        }
        catch (Exception ex)
        {
            return new JsonResult(
                                                  new
                                                  {
                                                      correct = false,
                                                      code = 500,
                                                      message = "Error al obtener el perfil: " + ex.Message,
                                                      data = new ProfileDTO()
                                                  });
        }

    }

    [HttpGet("GetProfiles")]
    public async Task<IActionResult> GetProfiles()
    {
        try
        {
            var users = await _usersProvider.GetProfiles();
            if (users != null)
                return new JsonResult(
                                       new
                                       {
                                           correct = true,
                                           code = 200,
                                           message = "",
                                           list = users
                                       });
            else
                return new JsonResult(
                                       new
                                       {
                                           correct = false,
                                           code = 401,
                                           message = "Ocurrio un error al buscar los usuarios",
                                           list = new ProfileDTO()
                                       });
        }
        catch (Exception ex)
        {
            return new JsonResult(
                                                  new
                                                  {
                                                      correct = false,
                                                      code = 500,
                                                      message = "Error al obtener el perfil: " + ex.Message,
                                                      list = new ProfileDTO()
                                                  });
        }

    }
    [HttpPost("UpgradeToUvComunity")]
    public async Task<IActionResult> UpgradeToUvComunity(NewUvComunityDTO user)
    {
        try
        {
            var result = await _usersProvider.UpgradeToUvComunity(user);
            if (result != ErrorCodes.ERROR)
                return new JsonResult(
                                                      new
                                                      {
                                                          correct = true,
                                                          code = 200,
                                                          message = "",
                                                      });
            else
                return new JsonResult(
                                                      new
                                                      {
                                                          correct = false,
                                                          code = 401,
                                                          mensaje = "No existe un usuario con ese ID, favor de verificar",
                                                      });
        }
        catch (Exception ex)
        {
            return new JsonResult(
                                                new
                                                {
                                                    correct = false,
                                                    code = 500,
                                                    message = "Error al obtener el usuario: " + ex.Message,
                                                });
        }

    }

}