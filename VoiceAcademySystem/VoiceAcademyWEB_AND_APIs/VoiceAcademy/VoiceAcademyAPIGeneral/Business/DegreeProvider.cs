using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VoiceAcademyAPIGeneral.Business.Interface;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPIGeneral.Business
{
    public class DegreeProvider : IDegreeProvider
    {
        private readonly VoiceacademydbContext _context;

        public DegreeProvider(VoiceacademydbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }
        public async Task<List<DegreeDTO>> GetDegrees()
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var list = await _context.Degrees
                                    .Include(d => d.FacultyIdFacultyNavigation)
                                    .ThenInclude(f => f.RegionIdRegionNavigation)
                                    .OrderBy(c => c.IdDegree)
                                    .ToListAsync();

                var newList = new List<DegreeDTO>();
                foreach (var item in list)
                {
                    var newDegree = new DegreeDTO()
                    {
                        IdDegree = item.IdDegree,
                        Name = item.Name,
                        IdFaculty = item.FacultyIdFaculty,
                        Faculty = item.FacultyIdFacultyNavigation.Name,
                        Region = item.FacultyIdFacultyNavigation.RegionIdRegionNavigation.Name

                    };
                    newList.Add(newDegree);
                }
                return newList;
            }
        }

        public async Task<List<DegreeDTO>> GetDegreesByFacultyId(int FacultyId)
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var list = await _context.Degrees.Where(x => x.FacultyIdFaculty==FacultyId).ToListAsync();

                var newList = new List<DegreeDTO>();
                foreach (var item in list)
                {
                    var newDegree = new DegreeDTO()
                    {
                        IdDegree = item.IdDegree,
                        Name = item.Name,
                        IdFaculty = item.FacultyIdFaculty,
                    };
                    newList.Add(newDegree);
                }
                return newList;
            }
        }
    }
}
