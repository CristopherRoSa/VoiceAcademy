using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPI.Utility;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPI.Business
{
    public class PodcastProvider : IPodcastProvider
    {
        private readonly VoiceacademydbContext _context;
        public PodcastProvider(VoiceacademydbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }

        public async Task<int> AddPodcast(PodcastDTO newPodcastDTO)
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            var result = ErrorCodes.ERROR;
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var idUvComunity = await _context.Uvcomunities.Where(u => u.UserIdUser == newPodcastDTO.UvcomunityIdUvcomunity).Select(u => u.IdUvcomunity).FirstOrDefaultAsync();
                if (idUvComunity != 0)
                {
                    var podcast = await _context.Podcastchannels.Where(x => x.Title == newPodcastDTO.Title && x.UvcomunityIdUvcomunity == idUvComunity).FirstOrDefaultAsync();
                    if (podcast == null)
                    {
                        Podcastchannel newPodcast = new Podcastchannel
                        {
                            Title = newPodcastDTO.Title,
                            Description = newPodcastDTO.Description,
                            StatePodcast = 1,
                            Topic = newPodcastDTO.Topic,
                            CreationDate = DateTime.Now,
                            UvcomunityIdUvcomunity = idUvComunity,
                            ImagePodcast = newPodcastDTO.ImagePodcast
                        };
                        await _context.Podcastchannels.AddAsync(newPodcast);
                        result = await _context.SaveChangesAsync();
                    }
                }
            }
            return result;
        }

        public async Task<int> ChangePodcastVisibility(int idPodcast)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var podcast = await _context.Podcastchannels.Where(x => x.IdPodcast == idPodcast).FirstOrDefaultAsync();
                if (podcast != null)
                {
                    podcast.StatePodcast = podcast.StatePodcast == 1 ? 0 : 1;
                    result = _context.SaveChanges();
                }
            }
            return result;
        }

        public async Task<int> DeletePodcast(int idPodcast)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var podcast = await _context.Podcastchannels.FirstOrDefaultAsync(x => x.IdPodcast == idPodcast);
                if (podcast != null)
                {
                    _context.Podcastchannels.Remove(podcast);
                    result = await _context.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<Podcastchannel> GetPodcastById(int idPodcast)
        {
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                return await _context.Podcastchannels.Where(x => x.IdPodcast == idPodcast).FirstOrDefaultAsync();
            }
        }

        public async Task<List<Podcastchannel>> GetPodcastByUserId(int idUser)
        {
            var result = new List<Podcastchannel>();
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var idUvComunity = await _context.Uvcomunities.Where(u => u.UserIdUser == idUser).Select(u => u.IdUvcomunity).FirstOrDefaultAsync();
                result = await _context.Podcastchannels.Where(x => x.UvcomunityIdUvcomunity == idUvComunity).ToListAsync();
            }
            return result;
        }

        public async Task<List<Podcastchannel>> GetPodcasts()
        {
            var result = new List<Podcastchannel>();
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                result = await _context.Podcastchannels.ToListAsync();
            }
            return result;
        }

        public async Task<int> UpdatePodcast(NewPodcastDTO newPodcastDTO, int idPodcast)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var podcast = await _context.Podcastchannels.Where(x => x.IdPodcast == idPodcast).FirstOrDefaultAsync();
                if (podcast != null)
                {
                    podcast.Title = newPodcastDTO.Title;
                    podcast.Description = newPodcastDTO.Description;
                    result = _context.SaveChanges();
                }
            }
            return result;
        }
    }
}