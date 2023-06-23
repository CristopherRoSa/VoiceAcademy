using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VoiceAcademyAPIGeneral.Business;
using VoiceAcademyAPIGeneral.Business.Interface;
using VoiceAcademyAPIGeneral.DTOs;

namespace VoiceAcademyAPIGeneral.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RegionController
    {
        private readonly ILogger<RegionController> _logger;
        private IRegionProvider _regionProvider;

        public RegionController(IRegionProvider regionProvider, ILogger<RegionController> logger)
        {
            _logger = logger;
            _regionProvider = regionProvider;
        }

        [HttpGet("GetFacultiesByRegionId/{RegionId}")]
        public async Task<IActionResult> GetFacultiesByRegionId(int RegionId)
        {
            try
            {
                var list = await _regionProvider.GetFacultiesByRegionId(RegionId);
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
                                                                      message = "No existen regiones",
                                                                      list = new List<FacultyDTO>()
                                                                  });
            }
            catch (Exception ex)
            {
                return new JsonResult(
                                                                         new
                                                                         {
                                                                             correct = false,
                                                                             code = 500,
                                                                             message = "Error al obtener las Regiones: " + ex.Message,
                                                                             list = new List<FacultyDTO>()
                                                                         });
            }
        }

        [HttpGet("GetRegions")]
        public async Task<IActionResult> GetRegions()

        {
            try
            {
                var list = await _regionProvider.GetRegions();
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
                                                                      message = "No existen regiones",
                                                                      list = new List<RegionDTO>()
                                                                  });
            }
            catch (Exception ex)
            {
                return new JsonResult(
                                                                         new
                                                                         {
                                                                             correct = false,
                                                                             code = 500,
                                                                             message = "Error al obtener las Regiones: " + ex.Message,
                                                                             list = new List<RegionDTO>()
                                                                         });
            }
        }
    }
}
