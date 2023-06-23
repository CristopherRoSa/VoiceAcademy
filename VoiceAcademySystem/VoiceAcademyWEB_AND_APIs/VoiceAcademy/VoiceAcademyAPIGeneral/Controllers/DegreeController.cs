using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPI.Controllers;
using VoiceAcademyAPIGeneral.Business.Interface;
using VoiceAcademyAPIGeneral.DTOs;

namespace VoiceAcademyAPIGeneral.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DegreeController
    {
        private readonly ILogger<DegreeController> _logger;
        private IDegreeProvider _degreeProvider;

        public DegreeController(IDegreeProvider degreeProvider, ILogger<DegreeController> logger)
        {
            _logger = logger;
            _degreeProvider = degreeProvider;
        }

        [HttpGet("GetDegrees")]
        public async Task<IActionResult> GetDegrees()
        {
            try
            {
                var list = await _degreeProvider.GetDegrees();
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
                                                                      message = "No existen grados",
                                                                      list = new List<DegreeDTO>()
                                                                  });
            }
            catch (Exception ex)
            {
                return new JsonResult(
                                                                         new
                                                                         {
                                                                             correct = false,
                                                                             code = 500,
                                                                             message = "Error al obtener los grados: " + ex.Message,
                                                                             list = new List<DegreeDTO>()
                                                                         });
            }
        }

        [HttpGet("GetDegreesByFacultyId/{FacultyId}")]
        public async Task<IActionResult> GetDegreesByFacultyId(int FacultyId)
        {
            try
            {
                var list = await _degreeProvider.GetDegreesByFacultyId(FacultyId);
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
                                                                      message = "No existen grados",
                                                                      list = new List<DegreeDTO>()
                                                                  });
            }
            catch (Exception ex)
            {
                return new JsonResult(
                                                                         new
                                                                         {
                                                                             correct = false,
                                                                             code = 500,
                                                                             message = "Error al obtener los grados: " + ex.Message,
                                                                             list = new List<DegreeDTO>()
                                                                         });
            }
        }
    }
}
