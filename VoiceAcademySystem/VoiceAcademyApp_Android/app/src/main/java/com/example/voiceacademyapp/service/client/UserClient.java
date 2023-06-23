package com.example.voiceacademyapp.service.client;

import com.example.voiceacademyapp.api.LoginService;
import com.example.voiceacademyapp.api.UserService;
import com.example.voiceacademyapp.api.configuration.ConfigApi;

import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

public class UserClient {
    private final UserService apiService;
    private static UserClient instance = null;

    public UserClient() {
        Retrofit retrofit = new Retrofit.Builder().baseUrl(ConfigApi.getBaseUrl())
                .addConverterFactory(GsonConverterFactory.create())
                .build();
        apiService = retrofit.create(UserService.class);
    }

    public static synchronized UserClient getInstance() {
        if (instance == null) {
            instance = new UserClient();
        }
        return instance;
    }

    public UserService getApiService() {
        return apiService;
    }
}
