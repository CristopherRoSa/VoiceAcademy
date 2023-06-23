using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using VoiceAcademyAPILogin.Bussines.Interface;
using VoiceAcademyAPILogin.DTOs;
using VoiceAcademyAPILogin.Models;
using VoiceAcademyAPILogin.Utility;

namespace VoiceAcademyAPILogin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private ILoginProvider _usersProvider;

        public LoginController(ILoginProvider usersProvider, ILogger<LoginController> logger)
        {
            _logger = logger;
            _usersProvider = usersProvider;
        }

        [HttpPost("AuthorizeUser")]
        public async Task<IActionResult> Login([FromBody] LoginDTO infologin)
        {
            JsonResult result = new JsonResult(new { });
            try
            {
                User userInfo = await _usersProvider.Login(infologin);
                if (userInfo == null)
                {
                    Console.WriteLine("firstIf");

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
                    Console.WriteLine("firstElse");
                    if (userInfo.Password.Equals(infologin.Password))
                    {
                        Console.WriteLine("secondIf");

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
                        Console.WriteLine("secondElse");

                        result = new JsonResult(
                            new
                            {
                                correct = false,
                                StatusCode = 401,
                                message = "Usuario o Contraseña incorrecto, favor de verificar",
                                token = new TokenDTO(),
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("cantch");

                result = new JsonResult(
                        new
                        {
                            correct = false,
                            StatusCode = 500,
                            message = "Error al iniciar sesion: " + ex.Message,
                            token = new TokenDTO(),
                        });
            }

            return result;
        }
    }
}
