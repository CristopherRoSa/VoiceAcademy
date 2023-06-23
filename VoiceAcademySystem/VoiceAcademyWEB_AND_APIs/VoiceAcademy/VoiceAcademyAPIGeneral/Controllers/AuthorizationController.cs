namespace VoiceAcademyAPI.Controllers;
using VoiceAcademyAPI.Utility;
using Microsoft.AspNetCore.Mvc;
using VoiceAcademyAPI.DTOs;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPIGeneral.Models;

[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private IUsersProvider _usersProvider;

    public AuthorizationController(IUsersProvider usersProvider, ILogger<UserController> logger)
    {
        _logger = logger;
        _usersProvider = usersProvider;
    }
    [HttpPost("SendVerificationToken/{email}/{code}")]
    public IActionResult SendVerificationToken(string email, int code)
    {
        bool response = _usersProvider.SendVerificationToken(email, code);
        if (response)
        {
            return Ok();
        }
        else
        {
            return StatusCode(500, "Error al enviar el correo electrónico. \n Asegurate de ingresar un correo electronico registrado.");
        }
    }

    [HttpPost("AllreadyExistEmail")]
    public async Task<IActionResult> AllreadyExistEmailAsync(string email)
    {
        bool response = await _usersProvider.AllreadyExistEmail(email);
        if (response)
        {
            return Ok();
        }
        else
        {
            return StatusCode(404, "Error al enviar el correo electrónico. \n Asegurate de ingresar un correo electronico registrado.");
        }

    }

    [HttpPost("AllreadyExistInstitutionalEmail")]
    public async Task<IActionResult> AllreadyExistInstitutionalEmailAsync(string email)
    {
        bool response = await _usersProvider.AllreadyExistInstitutionalEmail(email);
        if (response)
        {
            return Ok();
        }
        else
        {
            return StatusCode(404, "No se encontro el correo electronico proporcionado.");
        }

    }

    [HttpPost("AuthorizeUser")]
    public async Task<IActionResult> Login([FromBody] LoginDTO infologin)
    {
        JsonResult result = new JsonResult(new { });
        User userInfo = await _usersProvider.Login(infologin);
        if (userInfo == null)
        {
            result = new JsonResult(
                new
                {
                    correct = false,
                    StatusCode = 401,
                    message = "No existe un usuario con ese nombre, favor de verificar",
                });
        }
        else
        {
            if (userInfo.Password.Equals(infologin.Password))
            {
                var token = new TokenDTO
                {
                    Token = "",
                    idUser = userInfo.IdUser,
                    Role = userInfo.Rol,
                    FullName = userInfo.Name + " " + userInfo.LastName
                };
                result = TokenGenerator.GetToken(token);
            }
            else
            {
                result = new JsonResult(
                    new
                    {
                        correct = false,
                        StatusCode = 401,
                        message = "Usuario o Contraseña incorrecto, favor de verificar",
                    });
            }
        }

        return result;
    }

    [HttpPost("AddUser")]
    public async Task<IActionResult> PostAddUser([FromBody] UserDTO user)
    {
        var newUser = new User
        {
            Name = user.name,
            LastName = user.lastName,
            Email = user.email,
            Password = user.password,
            Rol = "userGeneral",
            Age = user.age,
            StatusUser = 1
        };
        var result = await _usersProvider.AddUser(newUser);
        var resulttwo = await _usersProvider.CreateLikesList(user.email);
        if (result != ErrorCodes.ERROR && resulttwo != ErrorCodes.ERROR)
        {
            return new JsonResult(
                    new
                    {
                        correct = true,
                        StatusCode = 200,
                        message = "Registro exitoso.",
                    });
        }
        else
            return new JsonResult(
                new
                {
                    correct = false,
                    StatusCode = 401,
                    message = "Error al registrar el usuario."
                });
    }


}