using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPI.Utility;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPI.Business
{
    public class ChaptersProvider : IChaptersProvider
    {
        private readonly VoiceacademydbContext _context;

        public ChaptersProvider(VoiceacademydbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }
        public async Task<int> AddChapter(ChapterDTO newChapterDTO)
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            var result = ErrorCodes.ERROR;
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var chapters = await _context.Chapters.Where(x => x.Title == newChapterDTO.Title && x.PodcastIdPodcast == newChapterDTO.PodcastIdPodcast).FirstOrDefaultAsync();
                if (chapters == null)
                {
                    Chapter newChapter = new Chapter()
                    {
                        Title = newChapterDTO.Title,
                        Description = newChapterDTO.Description,
                        StateChapter = 1,
                        Topic = newChapterDTO.Topic,
                        PublicationDate = DateTime.Now,
                        ImageChapter = newChapterDTO.ImageChapter,
                        PodcastIdPodcast = newChapterDTO.PodcastIdPodcast,
                        LinkChapter = newChapterDTO.LinkChapter,
                    };
                    await _context.Chapters.AddAsync(newChapter);
                    result = await _context.SaveChangesAsync();
                }
            }
            return result;
        }

        public async Task<bool> DeleteChapter(int idChapter)
        {
            throw new NotImplementedException();
        }

        public async Task<Chapter> GetChapter(int idChapter)
        {
            throw new NotImplementedException();
        }

        public async Task<Chapter> GetChapterById(int idChapter)
        {
            var result = new Chapter();
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                result = await _context.Chapters.Where(x => x.IdChapter == idChapter).FirstOrDefaultAsync();

            }
            return result;
        }

        public async Task<List<Chapter>> GetChapters()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Chapter>> GetChaptersByPlaylist(int idPlaylist)
        {
            var listChapters = new List<Chapter>();
            bool canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                Playlist playlist = await _context.Playlists.Include(p => p.ChapterIdChapters)
                                               .FirstOrDefaultAsync(p => p.IdPlayList == idPlaylist);

                if (playlist != null)
                {
                    listChapters = playlist.ChapterIdChapters.ToList();

                }
            }
            return listChapters;
        }

        public async Task<List<Chapter>> GetChaptersByPodcast(int idPodcast)
        {
            var chapters = new List<Chapter>();
            bool canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                chapters = await _context.Chapters.Where(x => x.PodcastIdPodcast == idPodcast).ToListAsync();
            }
            return chapters;
        }

        public async Task<List<Chapter>> GetLikedChaptersByPodcastId(int idPodcast, int idUser)
        {
            var chapters = new List<Chapter>();
            bool canConnect = await _context.Database.CanConnectAsync();
            var likelist = new LikeslistDTO();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var list = await _context.Likeslists.Include(l => l.ChapterIdChapters.Where(c => c.PodcastIdPodcast == idPodcast)).Where(x => x.UserIdUser == idUser).FirstOrDefaultAsync();
                if (list != null)
                {
                    var newLikeList = new LikeslistDTO()
                    {
                        IdlikesList = list.IdlikesList,
                        UserIdUser = list.UserIdUser,
                        TotalChapters = list.TotalChapters,
                        Followers = list.Followers,
                        Chapters = (List<Chapter>)list.ChapterIdChapters
                    };
                    likelist = newLikeList;
                    chapters = likelist.Chapters;
                }
            }
            return chapters;
        }

        public async Task<List<Chapter>> GetLikedChaptersByUserId(int idUser)
        {
            var chapters = new List<Chapter>();
            bool canConnect = await _context.Database.CanConnectAsync();
            var likelist = new LikeslistDTO();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var list = await _context.Likeslists.Include(l => l.ChapterIdChapters).Where(x => x.UserIdUser == idUser).FirstOrDefaultAsync();
                if (list != null)
                {
                    var newLikeList = new LikeslistDTO()
                    {
                        IdlikesList = list.IdlikesList,
                        UserIdUser = list.UserIdUser,
                        TotalChapters = list.TotalChapters,
                        Followers = list.Followers,
                        Chapters = (List<Chapter>)list.ChapterIdChapters
                    };
                    likelist = newLikeList;
                    chapters = likelist.Chapters;
                }
            }
            return chapters;
        }

        public async Task<bool> UpdateChapter(NewChapterDTO newChapter)
        {
            throw new NotImplementedException();
        }
    }
}
