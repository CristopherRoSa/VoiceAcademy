using VoiceAcademyWEB.DTOs;
using VoiceAcademyWEB.Models;

namespace VoiceAcademyWEB.Business.Interface
{
    public interface IPlayListProvider
    {
        public Task<List<PlayList>> GetPlayListsByUser(int userId);
        public Task<PlayList> GetPlayListById(int playListId);
        public Task<bool> UpdatePlayList(PlayListDTO newPlayList);
        public Task<bool> DeletePlayList(int idPlayList);
        public Task<bool> AddPlayList(PlayListDTO newPlayList);
        public Task<LikeslistDTO> GetLikesList(int idLikelist);
        public Task<bool> LikeChapter(LikeChapterDTO like);
        public Task<bool> AddChapterToPlayList(ChapterPlaylistDTO list);
        public Task<bool> DeleteLikeChapter(LikeChapterDTO like);
        public Task<bool> DeleteChapterToPlayList(ChapterPlaylistDTO list);
        public Task<int> GetUserIdPlaylistOwner(int userId);
    }
}
