using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Mvc;

namespace VoiceAcademyAPI.Controllers
{
    public class AudioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}
