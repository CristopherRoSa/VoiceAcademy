package com.example.voiceacademyapp.api;

import com.example.voiceacademyapp.DTOs.ChapterPlaylistDTO;
import com.example.voiceacademyapp.DTOs.LikeChapterDTO;
import com.example.voiceacademyapp.DTOs.LikeslistDTO;
import com.example.voiceacademyapp.DTOs.PlayListDTO;
import com.example.voiceacademyapp.model.PlayList;

import java.util.List;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.DELETE;
import retrofit2.http.GET;
import retrofit2.http.POST;
import retrofit2.http.PUT;
import retrofit2.http.Path;

public interface PlaylistService {
    @GET("/api/PlayLists/GetPlayLists/{userId}")
    Call<List<PlayList>> getPlayListsByUser(@Path("userId") int userId);

    @GET("playlists/{playListId}")
    Call<PlayList> getPlayListById(@Path("playListId") int playListId);

    @PUT("/api/PlayLists/UpdatePlayList")
    Call<Boolean> updatePlayList(@Body PlayListDTO newPlayList);

    @DELETE("/api/PlayLists/DeletePlayList/{idPlayList}")
    Call<Boolean> deletePlayList(@Path("idPlayList") int idPlayList);

    @POST("/api/PlayLists/AddPlayList")
    Call<Boolean> addPlayList(@Body PlayListDTO newPlayList);

    @GET("/api/PlayLists/GetLikeListById/{idLikelist}")
    Call<LikeslistDTO> getLikesList(@Path("idLikelist") int idLikelist);

    @POST("/api/PlayLists/LikeChapter")
    Call<Boolean> likeChapter(@Body LikeChapterDTO like);

    @POST("/api/PlayLists/AddChapterToPlayList")
    Call<Boolean> addChapterToPlayList(@Body ChapterPlaylistDTO list);

    @DELETE("/api/PlayLists/DeleteLikeChapter")
    Call<Boolean> deleteLikeChapter(@Body LikeChapterDTO like);

    @DELETE("/api/PlayLists/DeleteChapterToPlayList")
    Call<Boolean> deleteChapterToPlayList(@Body ChapterPlaylistDTO list);

}
