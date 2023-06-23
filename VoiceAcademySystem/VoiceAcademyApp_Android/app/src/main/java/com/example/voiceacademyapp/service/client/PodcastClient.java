package com.example.voiceacademyapp.service.client;


import com.example.voiceacademyapp.api.LoginService;
import com.example.voiceacademyapp.api.PodcastService;
import com.example.voiceacademyapp.api.configuration.ConfigApi;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class PodcastClient {
    private final PodcastService apiService;
    private static PodcastClient instance = null;

    public PodcastClient() {
        Retrofit retrofit = new Retrofit.Builder().baseUrl(ConfigApi.getBaseUrl())
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        apiService = retrofit.create(PodcastService.class);
    }

    public static synchronized PodcastClient getInstance() {
        if (instance == null) {
            instance = new PodcastClient();
        }
        return instance;
    }

    public PodcastService getApiService() {
        return apiService;
    }
}
