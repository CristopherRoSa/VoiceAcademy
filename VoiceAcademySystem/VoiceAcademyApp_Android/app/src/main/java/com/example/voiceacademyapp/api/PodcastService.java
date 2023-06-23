package com.example.voiceacademyapp.api;

import com.example.voiceacademyapp.api.responds.GenericResponse;
import com.example.voiceacademyapp.model.PodcastChannel;
import com.example.voiceacademyapp.model.User;

import retrofit2.Call;
import retrofit2.http.GET;
import retrofit2.http.Header;
import retrofit2.http.Path;

public interface PodcastService {

    @GET("/api/Podcasts/GetPodcasts")
    Call<GenericResponse<PodcastChannel>> getPodcasts(@Header("Authorization") String token);
}
