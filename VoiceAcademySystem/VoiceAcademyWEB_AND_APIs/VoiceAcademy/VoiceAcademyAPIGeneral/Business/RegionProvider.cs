using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VoiceAcademyAPIGeneral.Business.Interface;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPIGeneral.Business
{
    public class RegionProvider : IRegionProvider
    {
        private readonly VoiceacademydbContext _context;
        public RegionProvider(VoiceacademydbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }

        public async Task<List<FacultyDTO>> GetFacultiesByRegionId(int IdRegion)
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var list = await _context.Regions.Include(f => f.Faculties).Where(x => x.IdRegion == IdRegion).FirstOrDefaultAsync();

                var newList = new List<FacultyDTO>();
                foreach (var item in list.Faculties) {
                    FacultyDTO facultyDTO = new FacultyDTO()
                    {
                        Name = item.Name,
                        IdFaculty = item.IdFaculty,
                        IdRegion = IdRegion,
                        RegionName = _context.Regions.Where(x => x.IdRegion == IdRegion).FirstOrDefault().Name,
                    };
                    newList.Add(facultyDTO);
                }
                return newList;
            }
        }

        public async Task<List<RegionDTO>> GetRegions()
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var list = await _context.Regions.ToListAsync();

                var newList = new List<RegionDTO>();
                foreach (var item in list)
                {
                    var newRegion = new RegionDTO()
                    {
                        IdRegion = item.IdRegion,
                        Name = item.Name,

                    };
                    newList.Add(newRegion);
                }
                return newList;
            }
        }
    }
}
