using VoiceAcademyAPI.DTOs;
using VoiceAcademyAPIGeneral.DTOs;
using VoiceAcademyAPIGeneral.Models;

namespace VoiceAcademyAPI.Business.Interface
{
    public interface IPlayListsProvider
    {
        public Task<int> AddPlayList(PlayListDTO newPlayList);
        public Task<bool> DeleteChapterFromPlayList(int idChapter, int idPlaylist);
        public Task<int> DeletePlayList(int idPlaylist);
        public Task<List<Playlist>> GetPlaylistsByUser(int idUser);
        public Task<List<Playlist>> GetPlaylists();
        public Task<int> UpdatePlayList(PlayListDTO newPlayList);
        public Task<Playlist> GetPlaylistById(int idPlaylist);
        public Task<LikeslistDTO> GetLikeListById(int idLikeList);
        public Task<int> AddChapterToLikeList(LikeChapterDTO like);
        public Task<int> AddChapterToPlayList(ChapterPlaylistDTO list);
        public Task<int> DeleteChapterToLikeList(LikeChapterDTO like);
        public Task<int> DeleteChapterToPlayList(ChapterPlaylistDTO list);
        public Task<int> GetUserIdPlaylistOwner(int playlistId);

    }
}
