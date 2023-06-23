using Microsoft.EntityFrameworkCore;
using System.Globalization;
using VoiceAcademyAPI.Business.Interface;
using VoiceAcademyAPI.DTOs;

using VoiceAcademyAPI.Utility;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPI.Business
{
    public class PlayListProvider : IPlayListsProvider
    {

        private readonly VoiceacademydbContext _context;

        public PlayListProvider(VoiceacademydbContext context)
        {
            string culture = "es-MX";
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
            _context = context;

        }

        public async Task<int> AddChapterToLikeList(LikeChapterDTO like)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var likelist = await _context.Likeslists
                .Include(ll => ll.ChapterIdChapters)
                .Where(x => x.UserIdUser == like.IdUser)
                .FirstOrDefaultAsync();

                if (likelist != null)
                {
                    var chapter = await _context.Chapters.Where(x => x.IdChapter == like.IdChapter).FirstOrDefaultAsync();
                    if (chapter != null)
                    {
                        if (!likelist.ChapterIdChapters.Any(c => c.IdChapter == chapter.IdChapter))
                        {
                            likelist.ChapterIdChapters.Add(chapter);

                            result = await _context.SaveChangesAsync();
                        }
                        else
                        {
                            throw new Exception("El capítulo ya está en la lista de Me gusta");
                        }
                    }
                }
            }

            return result;
        }
        public async Task<int> DeleteChapterToLikeList(LikeChapterDTO like)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var likelist = await _context.Likeslists
                    .Include(ll => ll.ChapterIdChapters)
                    .Where(x => x.UserIdUser == like.IdUser)
                    .FirstOrDefaultAsync();

                if (likelist != null)
                {
                    var chapter = await _context.Chapters
                        .Where(x => x.IdChapter == like.IdChapter)
                        .FirstOrDefaultAsync();

                    if (chapter != null)
                    {
                        var existingChapter = likelist.ChapterIdChapters.FirstOrDefault(c => c.IdChapter == chapter.IdChapter);

                        if (existingChapter != null)
                        {
                            likelist.ChapterIdChapters.Remove(existingChapter);

                            result = await _context.SaveChangesAsync();
                        }
                        else
                        {
                            throw new Exception("El capítulo no está en la lista de Me gusta");
                        }
                    }
                }
            }

            return result;

        }
        public async Task<int> AddChapterToPlayList(ChapterPlaylistDTO list)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var playlist = await _context.Playlists.FindAsync(list.IdPlaylist);
                var chapter = await _context.Chapters.FindAsync(list.IdChapter);

                if (playlist != null && chapter != null)
                {
                    // Asociar el capítulo a la lista de reproducción
                    playlist.ChapterIdChapters.Add(chapter);

                    result = await _context.SaveChangesAsync();
                }
            }

            return result;
        }

        public async Task<int> DeleteChapterToPlayList(ChapterPlaylistDTO list)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var playlist = await _context.Playlists
                                .Include(p => p.ChapterIdChapters)
                                .FirstOrDefaultAsync(p => p.IdPlayList == list.IdPlaylist);
                var chapter = await _context.Chapters.FindAsync(list.IdChapter);

                if (playlist != null && chapter != null)
                {
                    if (playlist.ChapterIdChapters.Contains(chapter))
                    {
                        // Eliminar la relación entre la lista de reproducción y el capítulo
                        playlist.ChapterIdChapters.Remove(chapter);

                        result = await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("El capítulo no está asociado a la lista de reproducción.");
                    }
                }
                else
                {
                    throw new Exception("La lista de reproducción o el capítulo no existen.");
                }
            }

            return result;
        }

        public async Task<int> AddPlayList(PlayListDTO newPlayList)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var uvUser = await _context.Uvcomunities.Where(x => x.UserIdUser == newPlayList.IdUser).FirstOrDefaultAsync();
                if (uvUser != null)
                {
                    var playlist = new Playlist
                    {
                        UvcomunityIdUvcomunity = uvUser.IdUvcomunity,
                        Name = newPlayList.Name,
                        Description = newPlayList.Description,
                        ImagePlayList = newPlayList.Imagen,
                        StateList = 1,
                        CreationDate = DateTime.Now,
                        Followers = 0
                    };
                    await _context.Playlists.AddAsync(playlist);
                    result = _context.SaveChanges();
                }
            }
            return result;
        }

        public async Task<bool> DeleteChapterFromPlayList(int idChapter, int idPlaylist)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeletePlayList(int idPlaylist)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var playlist = await _context.Playlists.FirstOrDefaultAsync(x => x.IdPlayList == idPlaylist);
                if (playlist != null)
                {
                    _context.Playlists.Remove(playlist);
                    result = await _context.SaveChangesAsync();

                }
            }
            return result;
        }

        public async Task<LikeslistDTO> GetLikeListById(int idLikeList)
        {
            bool canConnect = await _context.Database.CanConnectAsync();
            var likelist = new LikeslistDTO();
            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var list = await _context.Likeslists.Include(l => l.ChapterIdChapters).Where(x => x.IdlikesList == idLikeList).FirstOrDefaultAsync();
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
                }
            }
            return likelist;
        }

        public async Task<Playlist> GetPlaylistById(int idPlaylist)
        {
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                return await _context.Playlists.Where(x => x.IdPlayList == idPlaylist).FirstOrDefaultAsync();
            }
        }

        public Task<List<Playlist>> GetPlaylists()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Playlist>> GetPlaylistsByUser(int idUser)
        {
            var result = new List<Playlist>();
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var uvUser = await _context.Uvcomunities.Where(x => x.UserIdUser == idUser).FirstOrDefaultAsync();
                if (uvUser != null)
                {
                    result = await _context.Playlists.Where(x => x.UvcomunityIdUvcomunity == uvUser.IdUvcomunity).ToListAsync();
                }
            }
            return result;
        }

        public async Task<int> UpdatePlayList(PlayListDTO newPlayList)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var playlist = await _context.Playlists.Where(x => x.IdPlayList == newPlayList.IdPlayList).FirstOrDefaultAsync();
                if (playlist != null)
                {
                    if (newPlayList.Name != ""){ playlist.Name = newPlayList.Name; }
                    if (newPlayList.Description != ""){playlist.Description = newPlayList.Description; }
                    if (newPlayList.Imagen != null) { playlist.ImagePlayList = newPlayList.Imagen; }
                    result = _context.SaveChanges();
                }
            }
            return result;
        }

        public async Task<int> GetUserIdPlaylistOwner(int playlistId)
        {
            var result = ErrorCodes.ERROR;
            bool canConnect = await _context.Database.CanConnectAsync();

            if (!canConnect)
            {
                throw new Exception("No se pudo establecer conexión con la base de datos.");
            }
            else
            {
                var uvComunityId = 0;
                    uvComunityId = await _context.Playlists.Where(x => x.IdPlayList == playlistId).Select(y => y.UvcomunityIdUvcomunity).FirstOrDefaultAsync();
                if (uvComunityId != 0)
                {
                    var ownerUserId = await _context.Uvcomunities.Where(x => x.IdUvcomunity == uvComunityId).Select(y => y.UserIdUser).FirstOrDefaultAsync();
                    result = ownerUserId;
                }

            }
            return result;
        }
    }
}
